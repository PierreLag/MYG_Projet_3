using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHUDScript : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI texteScore;
    [SerializeField]
    protected TextMeshProUGUI texteTemps;

    [SerializeField]
    protected float timeOnLevel = 120f;
    protected int score = 0;

    void FixedUpdate()
    {
        timeOnLevel -= Time.fixedDeltaTime;
        texteTemps.SetText("Temps\n" + Mathf.Floor(timeOnLevel/60) + ":" + Mathf.Floor(timeOnLevel%60f));
    }

    public void AddTime(float extraTime)
    {
        timeOnLevel += extraTime;
    }

    public void AddScore(int bonusPoints)
    {
        score += bonusPoints;
        texteScore.SetText("Score\n" + score);
    }
}
