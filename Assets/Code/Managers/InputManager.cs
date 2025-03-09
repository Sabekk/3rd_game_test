using Gameplay.GameStates;

namespace Gameplay.Inputs
{
    public class InputManager : GameplayManager<InputManager>
    {
        #region VARIABLES

        static InputBinds _controll;

        #endregion

        #region PROPERTIES

        public UIInputs UiInputs { get; private set; }
        public CharacterInputs CharacterInputs { get; private set; }
        public PauseInputs PauseInputs { get; private set; }
        public static InputBinds Input
        {
            get
            {
                if (_controll == null)
                    _controll = new InputBinds();
                return _controll;
            }
        }


        #endregion

        #region UNITY_METHODS

        private void OnEnable() => Input.Enable();

        private void OnDisable() => Input.Disable();

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            UiInputs = new(Input);
            CharacterInputs = new(Input);
            PauseInputs = new(Input);
        }

        public override void LateInitialzie()
        {
            base.LateInitialzie();
            RefreshInputs();
        }

        protected override void AttachEvents()
        {
            if (GameStateManager.Instance)
            {
                GameStateManager.Instance.OnCurrentGameStated += HandleCurrentGameStated;
            }
        }

        protected override void DetachEvents()
        {
            if (GameStateManager.Instance)
            {
                GameStateManager.Instance.OnCurrentGameStated -= HandleCurrentGameStated;
            }
        }

        private void RefreshInputs()
        {
            if (GameStateManager.Instance)
                switch (GameStateManager.Instance.CurrentGameState)
                {
                    case GameStateType.PLAYING:
                        CharacterInputs.Enable();
                        UiInputs.Enable();
                        PauseInputs.Disable();
                        break;
                    case GameStateType.WINDOW_VIEW:
                        CharacterInputs.Disable();
                        UiInputs.Enable();
                        PauseInputs.Disable();
                        break;
                    case GameStateType.PAUSE:
                        CharacterInputs.Disable();
                        UiInputs.Disable();
                        PauseInputs.Enable();
                        break;
                    default:
                        CharacterInputs.Enable();
                        UiInputs.Enable();
                        PauseInputs.Disable();
                        break;
                }
        }

        #region HANDLERS

        private void HandleCurrentGameStated()
        {
            RefreshInputs();
        }

        #endregion

        #endregion
    }
}
