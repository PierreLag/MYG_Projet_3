using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField]
    protected float openingSpeed = 0.2f;
    [SerializeField]
    protected float closingSpeed = 0.2f;
    [SerializeField]
    protected float distance = 10f;

    protected GameObject bars;
    private bool isClosing;

    private void Awake()
    {
        bars = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        if (isClosing)
        {
            bars.transform.position.Set(Mathf.MoveTowards(bars.transform.position.x, 0f, closingSpeed), bars.transform.position.y, bars.transform.position.z);
        }
        else
        {
            bars.transform.position.Set(Mathf.MoveTowards(bars.transform.position.x, distance, openingSpeed), bars.transform.position.y, bars.transform.position.z);
        }
    }

    public void Open()
    {
        isClosing = false;
    }

    public void Close()
    {
        isClosing = true;
    }
}
