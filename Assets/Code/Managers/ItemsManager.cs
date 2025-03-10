using Gameplay.Character;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Items
{
    public class ItemsManager : GameplayManager<ItemsManager>
    {

        #region VARIABLES

        private GameServerMock gameServerMock;

        #endregion

        #region PROPERTIES

        private CancellationTokenSource TokenSource { get; set; }
        public GameServerMock GameServerMock
        {
            get
            {
                if (gameServerMock == null)
                    gameServerMock = new();
                return gameServerMock;
            }
        }

        #endregion


        #region UNITY_METHODS

        private void OnDestroy()
        {
            CleanUp();
        }

        #endregion

        #region METHODS

        public override void CleanUp()
        {
            if (TokenSource != null)
            {
                if (!TokenSource.IsCancellationRequested)
                    TokenSource.Cancel();
                TokenSource.Dispose();
            }

            base.CleanUp();
        }

        public async Task<List<Item>> GetRandomItems()
        {
            List<Item> items = new();

            if (TokenSource == null || TokenSource.IsCancellationRequested)
                TokenSource = new CancellationTokenSource();

            try
            {
                string itemsData = await GameServerMock.GetItemsAsync(TokenSource.Token);

                if (TokenSource.IsCancellationRequested == true)
                    return items;

                JObject jObjectOfItems = JObject.Parse(itemsData);
                if (jObjectOfItems.TryGetValue("Items", out var jToken))
                {
                    var children = jToken.Children<JObject>();
                    foreach (var child in children)
                    {
                        Item item = new(child);
                        if (item.Data == null)
                        {
                            Debug.LogError($"Missing data in database for {item.ItemName}, check settings of database");
                            continue;
                        }
                        items.Add(item);
                    }
                }
            }
            catch (TaskCanceledException) 
            {
                Debug.LogWarning("Task of taking items canceled");
                return items;
            }
            catch (Exception e)
            {
                Debug.LogError($"Unexpected error: {e}");
                return items;
            }


            return items;
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            if (CharacterManager.Instance)
            {
                CharacterManager.Instance.OnPlayerCreated += HandlePlayerCreated;
            }
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            if (CharacterManager.Instance)
            {
                CharacterManager.Instance.OnPlayerCreated -= HandlePlayerCreated;
            }
        }


        [Button]
        private async void GiveRandomItemsToPlayer()
        {
            if (CharacterManager.Instance.Player == null)
                return;

            List<Item> itemsToAdd = await GetRandomItems();
            itemsToAdd.ForEach(item => CharacterManager.Instance.Player.EquipmentController.CollectItem(item));
        }

        #region HANDLERS

        private void HandlePlayerCreated()
        {
            GiveRandomItemsToPlayer();
        }

        #endregion

        #endregion
    }
}