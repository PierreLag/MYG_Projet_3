using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Level;
using Interactables;

namespace CustomUI
{
    public class LevelHUDScript : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI texteScore;
        [SerializeField]
        protected TextMeshProUGUI texteTemps;
        [SerializeField]
        protected TextMeshProUGUI texteCollectibles;
        [SerializeField]
        protected TextMeshProUGUI texteGameOver;
        [SerializeField]
        protected GameObject ingameUI;
        [SerializeField]
        protected GameObject ecranPause;
        [SerializeField]
        protected GameObject ecranFinPartie;

        protected LevelController level;
        protected int totalCollectibles;

        protected static LevelHUDScript s_levelHudScript;
        protected static GameObject st_ecranPause;
        protected static GameObject st_ecranFinPartie;
        protected static GameObject currentEcranPause;

        private void Awake()
        {
            s_levelHudScript = this;
            st_ecranPause = ecranPause;
            st_ecranFinPartie = ecranFinPartie;
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            totalCollectibles = CoinController.GetCoinAmount() + DiamondController.GetDiamondAmount() + TimerController.GetTimerAmount();
        }

        void FixedUpdate()
        {
            if (level == null)
            {
                level = LevelController.GetCurrentInstance();
            }
            float time = level.GetTime();
            texteTemps.SetText("Temps\n" + Mathf.Floor(time / 60f) + ":" + Mathf.Floor(time % 60f / 10) + Mathf.Floor(time % 10f));
            texteScore.SetText("Score\n" + level.GetScore());
            texteCollectibles.SetText("Collecté\n" + level.GetCollectedCollectibles() + "/" + totalCollectibles);
        }

        public static void Pause()
        {
            currentEcranPause = Instantiate(st_ecranPause, s_levelHudScript.transform);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public static void Unpause()
        {
            Destroy(currentEcranPause);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public async static void GameOver()
        {
            s_levelHudScript.texteGameOver.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            await Task.Delay(1000);

            s_levelHudScript.ingameUI.SetActive(false);
            Instantiate(st_ecranFinPartie, s_levelHudScript.transform);
        }
    }
}