using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using PlayerScripts;

namespace Interactables
{
    public class DiamondController : MonoBehaviour
    {
        [Serializable]
        protected class PickUpEvent : UnityEvent<int> { }

        [SerializeField]
        protected PickUpEvent OnPickUp;
        [SerializeField]
        protected int value;
        [SerializeField]
        protected float rotationSpeed = 20f;

        private Transform m_transform;

        private static int diamondAmount = 0;

        void Awake()
        {
            diamondAmount++;
            m_transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            m_transform.Rotate(0, Time.fixedDeltaTime * rotationSpeed, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player;
            if (other.gameObject.TryGetComponent<PlayerController>(out player))
            {
                OnPickUp.Invoke(value);
            }
        }

        public void ResetDiamondAmount()
        {
            diamondAmount = 0;
        }

        public static int GetDiamondAmount()
        {
            return diamondAmount;
        }
    }
}