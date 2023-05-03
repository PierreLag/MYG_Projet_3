using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace Level
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField]
        protected string loadingScene;

        protected static string st_loadingScene;

        void Awake()
        {
            st_loadingScene = loadingScene;
        }

        public async static void ChangeScene(string newSceneName)
        {
            SceneManager.LoadScene(st_loadingScene);

            await Task.Delay(1000);
            Interactables.CoinController.ResetCoinAmount();
            Interactables.TimerController.ResetTimerAmount();
            Interactables.DiamondController.ResetDiamondAmount();

            SceneManager.LoadSceneAsync(newSceneName);
        }
    }
}