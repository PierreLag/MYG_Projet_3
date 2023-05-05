using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileScripts;
using TMPro;

namespace CustomUI
{
    public class LeaderboardController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject scoreDisplay;
        [SerializeField]
        protected GameObject viewContent;

        protected Score[] scores;

        private void Awake()
        {
            scores = ScoreboardManager.ReadScore();
            int maxScore = ScoreboardManager.GetMaxScore();

            RectTransform viewRect = viewContent.GetComponent<RectTransform>();
            RectTransform scoreRect = scoreDisplay.GetComponent<RectTransform>();

            if (maxScore >= 5)
                viewRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250 + (maxScore - 5) * scoreRect.rect.height);
            else
                viewRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250);
        }

        // Start is called before the first frame update
        void Start()
        {
            RectTransform viewRect = viewContent.GetComponent<RectTransform>();

            for (int i = 0; i < scores.Length; i++)
            {
                GameObject currentScore = Instantiate(scoreDisplay, viewContent.transform);
                RectTransform currentRect = currentScore.GetComponent<RectTransform>();
                currentRect.localPosition = new Vector3(currentRect.localPosition.x, viewRect.localPosition.y - (currentRect.rect.height /2) - (currentRect.rect.height * i));

                currentScore.GetComponent<TextMeshProUGUI>().text = (i + 1) + " -\t" + scores[i].score + "\t" + scores[i].name + "\t" + scores[i].day.day + "/" + scores[i].day.month + "/" + scores[i].day.year;
            }
        }
    }
}