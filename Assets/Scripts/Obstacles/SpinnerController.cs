using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    [SerializeField]
    protected float spinningSpeed = 10f;

    protected Transform m_transform;

    void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_transform.Rotate(0, Time.deltaTime * spinningSpeed, 0);
    }
}
