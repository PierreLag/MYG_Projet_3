using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayerScripts;

namespace Interactables {
    public class CoinController : MonoBehaviour
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
            m_transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player;
            if (other.gameObject.TryGetComponent<PlayerController>(out player))
            {
                OnPickUp.Invoke(value);
            }
        }
    }
}