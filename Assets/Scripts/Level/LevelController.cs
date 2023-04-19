using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        static protected LevelController s_instance = null;

        [SerializeField]
        private float time = 120f;

        private int score = 0;

        private void OnEnable()
        {
            s_instance = this;
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

        public int GetScore()
        {
            return s_instance.score;
        }

        public float GetTime()
        {
            return s_instance.time;
        }

        void FixedUpdate()
        {
            s_instance.time -= Time.fixedDeltaTime;
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