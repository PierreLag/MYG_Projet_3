using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    [SerializeField]
    protected Vector3[] pointsPassage;
    [SerializeField]
    [Range(0f, 1f)]
    protected float offsetPosition = 0f;
    [SerializeField]
    protected bool isLooping = true;

    protected float totalDistance;
    protected float[] distanceList;

    // Start is called before the first frame update
    void Awake()
    {
        totalDistance = 0;
        if (isLooping)
        {
            distanceList = new float[pointsPassage.Length];
            for (int i = 1; i < pointsPassage.Length; i++)
            {
                distanceList[i - 1] = (pointsPassage[i] - pointsPassage[i - 1]).magnitude;
                totalDistance += distanceList[i - 1];
            }
            distanceList[distanceList.Length - 1] = (pointsPassage[0] - pointsPassage[pointsPassage.Length - 1]).magnitude;
            totalDistance += distanceList[distanceList.Length - 1];
        }
        else
        {
            distanceList = new float[pointsPassage.Length - 1];
            for (int i = 1; i < pointsPassage.Length; i++)
            {
                distanceList[i - 1] = (pointsPassage[i] - pointsPassage[i - 1]).magnitude;
                totalDistance += distanceList[i - 1];
            }
            totalDistance *= 2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
