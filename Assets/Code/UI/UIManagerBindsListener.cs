using Gameplay.Inputs;
using UI.Window;
using UI.Window.Inventory;

namespace UI
{
    public class UIManagerBindsListener
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        UIManager Manager { get; set; }

        #endregion

        #region METHODS

        public void Initialize(UIManager manager)
        {
            Manager = manager;
            AttachEvents();
        }

        public void CleanUp()
        {
            DetachEvents();
        }

        private void AttachEvents()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.UiInputs.OnToggleInventory += HandleToggleInventory;
            }
        }

        private void DetachEvents()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.UiInputs.OnToggleInventory -= HandleToggleInventory;
            }
        }

        #region HANDLERS

        private void HandleToggleInventory()
        {
            if (Manager.IsOpenened<UIInventoryWindow>())
                Manager.CloseWindow<UIInventoryWindow>();
            else
                Manager.OpenWindow<UIInventoryWindow>("InventoryWindow");
        }

        #endregion

        #endregion
    }
}