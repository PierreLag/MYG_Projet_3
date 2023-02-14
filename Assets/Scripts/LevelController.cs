using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private float time = 120f;

        private int score = 0;

        public void AddScore(int bonusPoints)
        {
            score += bonusPoints;
        }

        public void AddTime(float bonusTime)
        {
            time += bonusTime;
        }

        public int GetScore()
        {
            return score;
        }

        public float GetTime()
        {
            return time;
        }

        void FixedUpdate()
        {
            time -= Time.fixedDeltaTime;
        }
    }
}