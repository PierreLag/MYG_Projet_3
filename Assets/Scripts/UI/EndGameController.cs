using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Level;
using FileScripts;

namespace CustomUI
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI texteScore;
        [SerializeField]
        protected TextMeshProUGUI texteTemps;
        [SerializeField]
        protected TextMeshProUGUI texteScoreFinal;
        [SerializeField]
        protected TextMeshProUGUI texteNomJoueur;

        [SerializeField]
        protected GameObject nomJoueur;
        [SerializeField]
        protected GameObject continuation;

        private LevelController currentLevel;
        private int scoreFinal = 0;

        private void Awake()
        {
            currentLevel = LevelController.GetCurrentInstance();
        }

        // Start is called before the first frame update
        void Start()
        {
            DisplayScore();
        }

        private async void DisplayScore()
        {
            await Task.Delay(500);
            texteScore.SetText("Score obtenu :\n" + currentLevel.GetScore());

            await Task.Delay(500);
            texteTemps.SetText("Temps restant :\n" + Mathf.Floor(currentLevel.GetTime() / 60f) + ":" + Mathf.Floor(currentLevel.GetTime() % 60f / 10) + Mathf.Floor(currentLevel.GetTime() % 10f));

            scoreFinal = currentLevel.GetScore() + (int)Mathf.Floor(currentLevel.GetTime() * 100);

            await Task.Delay(500);
            texteScoreFinal.SetText("Score obtenu :\n" + scoreFinal);

            await Task.Delay(500);
            nomJoueur.SetActive(true);
        }

        public void StoreScore()
        {
            ScoreboardManager.StoreScore(scoreFinal, texteNomJoueur.text);

            nomJoueur.SetActive(false);
            continuation.SetActive(true);
        }
    }
}