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

        private static string st_filePathName;

        [Serializable]
        private class Score
        {
            public int score;
            public string name;
            public string day;

            public static Score[] CreateArrayFromJSON(string json)
            {
                return JsonUtility.FromJson<Score[]>(json);
            }

            public static string CreateJSONFromArray(Score[] scoreboard)
            {
                return JsonUtility.ToJson(scoreboard);
            }
        }

        [STAThread]
        private void Awake()
        {
            st_filePathName = filePathName;

            try
            {
                StreamReader sr = new StreamReader(filePathName);
                string text = sr.ReadLine();

                if (text == "")
                {
                    sr.Close();

                    StreamWriter sw = new StreamWriter(filePathName);
                    sw.Write(Score.CreateJSONFromArray(new Score[10]));

                    sw.Close();
                }
                else
                {
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {

            }
        }

        [STAThread]
        public static void StoreScore(int score, string name)
        {

        }
    }
}