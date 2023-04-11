using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

namespace CustomEvents
{
    public class TimerEventController : MonoBehaviour
    {
        [Serializable]
        protected class CountdownEvent : UnityEvent { }
        [SerializeField]
        protected TextMeshPro display;
        [SerializeField]
        protected float timer = 30f;
        [SerializeField]
        protected bool isFrozen = false;

        [SerializeField]
        protected CountdownEvent OnCountdownReached;

        protected float countdown;

        private void Awake()
        {
            countdown = 0f;
            display.text = countdown.ToString();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (countdown > 0f && !isFrozen)
            {
                countdown -= Time.fixedDeltaTime;
                if (countdown <= 0f)
                {
                    countdown = 0f;
                    OnCountdownReached.Invoke();
                }
                display.text = Math.Truncate(countdown).ToString();
            }
        }

        public void ResetTimer()
        {
            countdown = timer;
        }

        public void Freeze()
        {
            isFrozen = true;
        }

        public void Unfreeze()
        {
            isFrozen = false;
        }
    }
}