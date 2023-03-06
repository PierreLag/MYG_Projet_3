using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
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
        private Vector3 initialPosition;

        private void Awake()
        {
            bars = transform.GetChild(0).gameObject;
            initialPosition = bars.transform.position;
            isClosing = true;
        }

        private void FixedUpdate()
        {
            if (isClosing)
            {
                bars.transform.position = new Vector3(Mathf.MoveTowards(bars.transform.position.x, initialPosition.x, closingSpeed), bars.transform.position.y, bars.transform.position.z);
            }
            else
            {
                bars.transform.position = new Vector3(Mathf.MoveTowards(bars.transform.position.x, initialPosition.x + distance, openingSpeed), bars.transform.position.y, bars.transform.position.z);
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
}