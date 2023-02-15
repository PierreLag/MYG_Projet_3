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

    protected LevelController level;

    void FixedUpdate()
    {
        if (level == null)
        {
            level = LevelController.GetCurrentInstance();
        }
        float time = level.GetTime();
        texteTemps.SetText("Temps\n" + Mathf.Floor(time / 60f) + ":" + Mathf.Floor(time % 60f / 10) + Mathf.Floor(time % 10f));
        texteScore.SetText("Score\n" + level.GetScore());
    }
}
