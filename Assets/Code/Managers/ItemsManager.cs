using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gameplay.Items
{
    public class ItemsManager : MonoSingleton<ItemsManager>
    {

        #region VARIABLES

        private GameServerMock gameServerMock = new();

        #endregion

        #region PROPERTIES

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


        #region METHODS

        [Button]
        public async Task<List<Item>> DebugItems()
        {
            List<Item> items = new();
            string itemsData = await GameServerMock.GetItemsAsync();
            JObject jObjectOfItems = JObject.Parse(itemsData);

            if (jObjectOfItems.TryGetValue("Items", out var jToken))
            {
                var children = jToken.Children<JObject>();
                foreach (var child in children)
                {
                    Item item = new(child);
                    items.Add(item);
                }
            }

            return items;
        }

        #endregion
    }
}