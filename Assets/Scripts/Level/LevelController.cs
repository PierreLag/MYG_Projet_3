using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using CustomUI;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        static protected LevelController s_instance = null;

        [SerializeField]
        private float time = 120f;

        private int score = 0;
        private int collectedCollectibles = 0;
        protected int totalCollectibles;

        private void OnEnable()
        {
            s_instance = this;
        }

        private void Start()
        {
            s_instance.totalCollectibles = CoinController.GetCoinAmount() + DiamondController.GetDiamondAmount() + TimerController.GetTimerAmount();
        }

        static public LevelController GetCurrentInstance()
        {
            return s_instance;
        }

        public void AddScore(int bonusPoints)
        {
            s_instance.score += bonusPoints;
        }

        public void AddTime(float bonusTime)
        {
            s_instance.time += bonusTime;
        }

        public void IncrementCollection()
        {
            s_instance.collectedCollectibles++;
        }

        public int GetScore()
        {
            return s_instance.score;
        }

        public float GetTime()
        {
            return s_instance.time;
        }

        public int GetCollectedCollectibles()
        {
            return s_instance.collectedCollectibles;
        }

        void FixedUpdate()
        {
            s_instance.time = Mathf.MoveTowards(s_instance.time, 0f, Time.fixedDeltaTime);

            if (s_instance.time == 0f || s_instance.collectedCollectibles == s_instance.totalCollectibles)
            {
                FreezeGame();
                LevelHUDScript.GameOver();
            }
        }

        public static void FreezeGame()
        {
            Time.timeScale = 0;
        }

        public static void UnfreezeGame()
        {
            Time.timeScale = 1;
        }
    }
}