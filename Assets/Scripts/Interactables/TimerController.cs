using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using PlayerScripts;

namespace Interactables
{
    public class TimerController : MonoBehaviour
    {
        [Serializable]
        protected class PickUpEvent : UnityEvent<float> { }

        [SerializeField]
        protected PickUpEvent OnPickUp;
        [SerializeField]
        protected float tempsExtra = 20f;
        [SerializeField]
        protected float rotationSpeed = 20f;

        private Transform m_transform;

        protected static int timerAmount = 0;

        void Awake()
        {
            timerAmount++;
            m_transform = GetComponent<Transform>();
        }

        void FixedUpdate()
        {
            m_transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player;
            if (other.gameObject.TryGetComponent<PlayerController>(out player))
            {
                OnPickUp.Invoke(tempsExtra);
            }
        }

        public void ResetTimerAmount()
        {
            timerAmount = 0;
        }

        public static int GetTimerAmount()
        {
            return timerAmount;
        }
    }
}