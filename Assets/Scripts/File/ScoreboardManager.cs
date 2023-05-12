using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace FileScripts
{
    public class ScoreboardManager : MonoBehaviour
    {
        [SerializeField]
        protected string filePathName = "./Scoreboard.json";
        [SerializeField]
        protected int maxScoreStorage = 10;
        [SerializeField]
        protected float timeScoreMultiplier = 5;

        private static string st_filePathName;
        private static int st_maxScoreStorage;
        private static float st_timeScoreMultiplier;
        
        [STAThread]
        private void Awake()
        {
            st_filePathName = filePathName;
            st_maxScoreStorage = maxScoreStorage;
            st_timeScoreMultiplier = timeScoreMultiplier;

            try
            {
                StreamReader sr = new StreamReader(filePathName);
                sr.Close();
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(FileNotFoundException))
                {
                    StreamWriter sw = new StreamWriter(filePathName);

                    Score[] defaultScoreboard = new Score[maxScoreStorage];

                    for (int i = 0; i < maxScoreStorage; i++)
                    {
                        defaultScoreboard[i] = new Score();
                        defaultScoreboard[i].day.minute += i;
                    }

                    sw.Write(Score.CreateJSONFromArray(defaultScoreboard, true));

                    sw.Close();
                }
            }
        }

        [STAThread]
        public static void StoreScore(int score, string name)
        {
            Score[] previousList = ReadScore();
            Score newScore = new Score(score, name);

            SortedList<double, Score> tempList = new SortedList<double, Score>(new DoubleDescOrder());

            foreach (Score previousScore in previousList)
            {
                tempList.Add(previousScore.score + (previousScore.day.year / 10000d) + (previousScore.day.month / 1000000d) + (previousScore.day.day / 100000000d)
                    + (previousScore.day.hour / 10000000000d) + (previousScore.day.minute / 1000000000000d) + (previousScore.day.second / 100000000000000d), previousScore);
            }
            tempList.Add(newScore.score + (newScore.day.year / 10000d) + (newScore.day.month / 1000000d) + (newScore.day.day / 100000000d)
                    + (newScore.day.hour / 10000000000d) + (newScore.day.minute / 1000000000000d) + (newScore.day.second / 100000000000000d), newScore);

            for (int i = 0; i < st_maxScoreStorage; i++)
            {
                previousList[i] = tempList.Values[i];
            }

            try
            {
                StreamWriter sw = new StreamWriter(st_filePathName, false);
                sw.Write(Score.CreateJSONFromArray(previousList, true));

                sw.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        [STAThread]
        public static Score[] ReadScore()
        {
            Score[] score = new Score[st_maxScoreStorage];
            try
            {
                StreamReader sr = new StreamReader(st_filePathName);
                string jsonScore = sr.ReadToEnd();

                sr.Close();

                score = Score.CreateArrayFromJSON(jsonScore);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return score;
        }

        public static int GetMaxScore()
        {
            return st_maxScoreStorage;
        }

        public static float GetTimeScoreMultiplier()
        {
            return st_timeScoreMultiplier;
        }
    }
}