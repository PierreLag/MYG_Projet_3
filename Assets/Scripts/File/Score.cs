using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FileScripts
{
    [Serializable]
    public class Score
    {
        public int score;
        public string name;
        public CustomDate day;

        public Score()
        {
            score = 0;
            name = "";
            day = new CustomDate();
        }

        public Score(int score, string name)
        {
            this.score = score;
            this.name = name;
            day = new CustomDate(DateTime.Now);
        }

        public Score(int score, string name, CustomDate day)
        {
            this.score = score;
            this.name = name;
            this.day = day;
        }

        public static Score[] CreateArrayFromJSON(string json)
        {
            ScoreWrapper wrapper = JsonUtility.FromJson<ScoreWrapper>(json);
            return wrapper.scores;
        }

        public static string CreateJSONFromArray(Score[] scoreboard)
        {
            ScoreWrapper wrapper = new ScoreWrapper();
            wrapper.scores = scoreboard;
            return JsonUtility.ToJson(wrapper);
        }

        public static string CreateJSONFromArray(Score[] scoreboard, bool prettyPrint)
        {
            ScoreWrapper wrapper = new ScoreWrapper();
            wrapper.scores = scoreboard;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class ScoreWrapper
        {
            public Score[] scores;
        }

        [Serializable]
        public class CustomDate
        {
            public int year;
            public int month;
            public int day;
            public int hour;
            public int minute;
            public int second;

            public CustomDate()
            {
                year = 1900;
                month = 1;
                day = 1;
                hour = 0;
                minute = 0;
                second = 0;
            }

            public CustomDate(DateTime date)
            {
                year = date.Year;
                month = date.Month;
                day = date.Day;
                hour = date.Hour;
                minute = date.Minute;
                second = date.Second;
            }
        }

    }
    public class DoubleDescOrder : Comparer<double>
    {
        public override int Compare(double x, double y)
        {
            return y.CompareTo(x);
        }
    }

}