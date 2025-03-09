using Core.Drawer;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Scenes
{
    public class ScenesManager : MonoSingleton<ScenesManager>
    {
        #region VARIABLES

        [SerializeField, SceneTag] string menuScene;
        [SerializeField, SceneTag] string gameplayScene;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public async void LoadMenuScene()
        {
            await LoadSceneAsync(menuScene);
        }

        public async void LoadGameplayScene()
        {
            await LoadSceneAsync(gameplayScene);
        }

        private async Task LoadSceneAsync(string sceneName)
        {
            await Task.Yield();

            AsyncOperation loadingScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            loadingScene.allowSceneActivation = false;

            while (loadingScene.isDone == false)
            {
                await Task.Yield();
            }

            loadingScene.allowSceneActivation = true;
        }

        #endregion
    }
}
