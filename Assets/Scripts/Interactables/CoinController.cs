using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    protected int value;
    private Transform m_transform;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.Rotate(0, Time.deltaTime * -20, 0);
    }
}
