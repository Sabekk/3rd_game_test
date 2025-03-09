using Core.Drawer;
using ObjectPooling;
using System.Threading.Tasks;
using UI;
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
            if (UIManager.Instance)
                UIManager.Instance.CloseAllWindow();

            await LoadSceneAsync(menuScene);
        }

        public async void LoadGameplayScene()
        {
            await LoadSceneAsync(gameplayScene);
        }

        private async Task LoadSceneAsync(string sceneName)
        {
            await Task.Yield();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
            //TODO make loading screen or progress bar with Progress from above AsyncOperation 
        }

        #endregion
    }
}
