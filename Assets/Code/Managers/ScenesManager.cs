using Core.Drawer;
using System;
using System.Threading;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Scenes
{
    public class ScenesManager : GameplayManager<ScenesManager>
    {
        #region ACTIONS

        public event Action OnSceneLoaded;

        #endregion

        #region VARIABLES

        [SerializeField, SceneTag] string menuScene;
        [SerializeField, SceneTag] string gameplayScene;

        #endregion

        #region PROPERTIES

        public string MenuScene => menuScene;
        public string GameplayScene => gameplayScene;
        private CancellationTokenSource TokenSource { get; set; }

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

        public async void LoadMenuScene()
        {
            if (UIManager.Instance)
                UIManager.Instance.CloseAllWindow();

            await LoadSceneAsync(menuScene);
        }

        public async void LoadGameplayScene()
        {
            await LoadSceneAsync(gameplayScene);
        }

        public SceneType GetCurrentSceneType()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == MenuScene)
                return SceneType.MENU;
            else if (currentSceneName == GameplayScene)
                return SceneType.GAMEPLAY;

            return SceneType.NONE;
        }

        private async Task LoadSceneAsync(string sceneName)
        {
            TokenSource?.Cancel();
            TokenSource?.Dispose();
            TokenSource = new ();

            CancellationToken token = TokenSource.Token;

            await Task.Yield();
            var sceneLoading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            sceneLoading.allowSceneActivation = false;

            while (sceneLoading.isDone == false)
            {
                if (sceneLoading.progress >= 0.9f)
                {
                    break;
                }

                if (token.IsCancellationRequested)
                {
                    //TODO Add returning to menu scene etc
                    Debug.LogError("Loading scene failed");
                    return;
                }

                await Task.Yield();
            }

            sceneLoading.allowSceneActivation = true;

            OnSceneLoaded?.Invoke();
            //TODO make loading screen or progress bar with Progress from above AsyncOperation 
        }

        #endregion
    }
}
