using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Level;

namespace CustomUI
{
    public class LevelHUDScript : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI texteScore;
        [SerializeField]
        protected TextMeshProUGUI texteTemps;
        [SerializeField]
        protected GameObject ecranPause;

        protected LevelController level;

        protected static LevelHUDScript s_levelHudScript;
        protected static GameObject st_ecranPause;
        protected static GameObject currentEcranPause;

        private void Awake()
        {
            s_levelHudScript = this;
            st_ecranPause = ecranPause;
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
    }
}