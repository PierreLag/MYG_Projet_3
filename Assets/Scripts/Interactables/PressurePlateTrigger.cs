using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Interactables
{
    public class PressurePlateTrigger : MonoBehaviour
    {
        [Serializable]
        protected class PressureEvent : UnityEvent { }

        [SerializeField]
        [Range(0f, float.PositiveInfinity)]
        protected float skinWidth = 1f;
        [SerializeField]
        protected LayerMask triggeringLayers;
        [SerializeField]
        protected PressureEvent OnPressed;
        [SerializeField]
        protected PressureEvent OnRelease;

        protected Collider m_collider;

        private void Awake()
        {
            m_collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Vector3 positionCollider = other.ClosestPoint(transform.position);

            if (Physics.Raycast(transform.position, positionCollider - transform.position, skinWidth, triggeringLayers))
            {
                OnPressed.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Vector3 positionCollider = other.ClosestPoint(transform.position);

            if (Physics.Raycast(transform.position, positionCollider - transform.position, float.PositiveInfinity, triggeringLayers))
            {
                OnRelease.Invoke();
            }
        }
    }
}