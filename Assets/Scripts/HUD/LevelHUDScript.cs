using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Level;

public class LevelHUDScript : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI texteScore;
    [SerializeField]
    protected TextMeshProUGUI texteTemps;
    [SerializeField]
    protected LevelController level;

    void FixedUpdate()
    {
        float time = level.GetTime();
        texteTemps.SetText("Temps\n" + Mathf.Floor(time / 60f) + ":" + Mathf.Floor(time % 60f));
        texteScore.SetText("Score\n" + level.GetScore());
    }
}
