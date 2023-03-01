using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Interactables
{
    public class PressureSwitch : MonoBehaviour
    {
        [Serializable]
        protected class PressureEvent : UnityEvent { }

        [SerializeField]
        protected PressureEvent OnPressed;
        [SerializeField]
        protected PressureEvent OnRelease;
        [SerializeField]
        protected LayerMask triggeringLayers;

        private void OnTriggerEnter(Collider other)
        {
            if (triggeringLayers.value == other.gameObject.layer)
            {
                OnPressed.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (triggeringLayers.value == other.gameObject.layer)
            {
                OnRelease.Invoke();
            }
        }
    }
}