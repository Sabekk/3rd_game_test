using Database;
using Gameplay.Controller.Module;
using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Equipment
{
    public class VisualizationModule : ControllerModuleBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemEquipped += HandleItemEquipped;
            Character.EquipmentController.OnItemUnequipped += HandleItemRemoved;
            Character.EquipmentController.OnItemsReplaced += HandleItemsReplaced;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquipped -= HandleItemEquipped;
            Character.EquipmentController.OnItemUnequipped -= HandleItemRemoved;
            Character.EquipmentController.OnItemsReplaced -= HandleItemsReplaced;
        }


        private void ShowVisualozationForItem(Item item)
        {
            if (item == null)
                return;

            int idOfPool = item.Data.PoolVisualizationId;
            if (idOfPool == 0)
            {
                var itemCategoryData = MainDatabases.Instance.ItemsDatabase.GetCategory(item.Category);
                if (itemCategoryData == null)
                {
                    Debug.LogError($"Item category didn't found i database [{item.Category}]");
                    return;
                }
                idOfPool = itemCategoryData.DefaultItemData.PoolVisualizationId;
            }

            if (idOfPool == 0)
            {
                Debug.LogError($"Missing pool id in item setting [{item.ItemName}]. Check database");
                return;
            }

            foreach (var itemSocket in Character.CharacterInGame.ItemSockets)
            {
                if (itemSocket.CategoryItem == item.Category)
                {
                    var poolable = ObjectPooling.ObjectPool.Instance.GetFromPool(idOfPool, MainDatabases.Instance.ItemsDatabase.IdOfItemVisualizationsFromPool);
                    if (poolable == null)
                    {
                        Debug.LogError($"Missing pool id in item setting [{item.ItemName}]. Check database");
                        return;
                    }

                    ItemVisualizationBase visualization = poolable.GetComponent<ItemVisualizationBase>();
                    if (visualization)
                    {
                        if (item.Visualizations == null)
                            item.Visualizations = new();

                        item.Visualizations.Add(visualization);
                        visualization.Initialize(Character, item);
                        visualization.transform.SetParentWithReset(itemSocket.transform);
                    }
                    else
                        break;
                }
            }
        }

        private void HideVisualozationForItem(Item item)
        {
            if (item.Visualizations == null || item.Visualizations.Count == 0)
                return;

            foreach (var visualization in item.Visualizations)
                ObjectPooling.ObjectPool.Instance.ReturnToPool(visualization);

            item.Visualizations.Clear();
        }

        #region HANDLERS

        private void HandleItemEquipped(Item item)
        {
            ShowVisualozationForItem(item);
        }

        private void HandleItemRemoved(Item item)
        {
            HideVisualozationForItem(item);
        }

        private void HandleItemsReplaced(Item newItem, Item oldItem)
        {
            HideVisualozationForItem(oldItem);
            ShowVisualozationForItem(newItem);
        }

        #endregion

        #endregion
    }
}