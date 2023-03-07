using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace CustomEvents
{
    public class AND_EventController : MonoBehaviour
    {
        [Serializable]
        protected class TriggeredEvent : UnityEvent { }

        [SerializeField]
        protected Light lightTriggerOne;
        [SerializeField]
        protected Light lightTriggerTwo;
        [SerializeField]
        protected Light lightTriggerThree;

        [SerializeField]
        protected TriggeredEvent OnEnabled;
        [SerializeField]
        protected TriggeredEvent OnDisabled;

        protected bool isEventOneTriggered;
        protected bool isEventTwoTriggered;

        private void Awake()
        {
            isEventOneTriggered = false;
            isEventTwoTriggered = false;

            lightTriggerOne.enabled = false;
            lightTriggerTwo.enabled = false;
            lightTriggerThree.enabled = false;
        }

        public void EventOneEnable()
        {
            isEventOneTriggered = true;
            lightTriggerOne.enabled = true;

            if (isEventTwoTriggered)
            {
                OnEnabled.Invoke();
                lightTriggerThree.enabled = true;
            }
        }

        public void EventTwoEnable()
        {
            isEventTwoTriggered = true;
            lightTriggerTwo.enabled = true;

            if (isEventOneTriggered)
            {
                OnEnabled.Invoke();
                lightTriggerThree.enabled = true;
            }
        }

        public void EventOneDisable()
        {
            isEventOneTriggered = false;
            lightTriggerOne.enabled = false;

            if (isEventTwoTriggered)
            {
                OnDisabled.Invoke();
                lightTriggerThree.enabled = false;
            }
        }

        public void EventTwoDisable()
        {
            isEventTwoTriggered = false;
            lightTriggerTwo.enabled = false;

            if (isEventOneTriggered)
            {
                OnDisabled.Invoke();
                lightTriggerThree.enabled = false;
            }
        }
    }
}