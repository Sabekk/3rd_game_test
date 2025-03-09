using Gameplay.Inputs;
using UI.Window;
using UI.Window.Inventory;
using UI.Window.Pause;

namespace UI
{
    public class UIManagerBindsListener
    {
        #region VARIABLES

        const string INVENTORY_WINDOW = "InventoryWindow";
        const string PAUSE_WINDOW = "PauseWindow";

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
            if (MainManager.Instance.Initialized)
            {
                LateAttachEvents();
            }
            else if (MainManager.Instance != null)
            {
                MainManager.Instance.OnGameplayManagersInitialized += HandleGameplayManagersInitialized;
            }
        }

        private void DetachEvents()
        {
            if (MainManager.Instance != null)
            {
                MainManager.Instance.OnGameplayManagersInitialized -= HandleGameplayManagersInitialized;
            }

            DetachLateEvents();
        }

        private void LateAttachEvents()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.UiInputs.OnToggleInventory += HandleToggleInventory;
                InputManager.Instance.UiInputs.OnForceCloseLast += HandleForceCloseLast;

                InputManager.Instance.PauseInputs.OnTogglePause += HandleTogglePause;
            }
        }

        private void DetachLateEvents()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.UiInputs.OnToggleInventory -= HandleToggleInventory;
                InputManager.Instance.UiInputs.OnForceCloseLast -= HandleForceCloseLast;

                InputManager.Instance.PauseInputs.OnTogglePause -= HandleTogglePause;
            }
        }

        #region HANDLERS

        private void HandleGameplayManagersInitialized()
        {
            LateAttachEvents();
        }

        private void HandleToggleInventory()
        {
            if (Manager.IsOpenened<UIInventoryWindow>())
                Manager.CloseWindow<UIInventoryWindow>();
            else
                Manager.OpenWindow<UIInventoryWindow>(INVENTORY_WINDOW);
        }

        private void HandleTogglePause()
        {
            if (Manager.IsOpenened<UIPauseWindow>())
                Manager.CloseWindow<UIPauseWindow>();
            else
                Manager.OpenWindow<UIPauseWindow>(PAUSE_WINDOW);
        }

        private void HandleForceCloseLast()
        {
            if (Manager.AnyWindowIsOpened == false)
                Manager.OpenWindow<UIPauseWindow>(PAUSE_WINDOW);
            else
                Manager.TryCloseLastOpenedWindow();
        }

        #endregion

        #endregion
    }
}