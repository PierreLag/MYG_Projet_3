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

        public void Press()
        {
            OnPressed.Invoke();
        }

        public void Release()
        {
            OnRelease.Invoke();
        }
    }
}