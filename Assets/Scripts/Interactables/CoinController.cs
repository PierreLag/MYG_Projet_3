using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayerScripts;

namespace Interactables {
    public class CoinController : MonoBehaviour
    {
        [SerializeField]
        protected Material materialBronze;
        [SerializeField]
        protected Material materialSilver;
        [SerializeField]
        protected Material materialGold;

        [Serializable]
        protected class PickUpEvent : UnityEvent<int> { }

        [SerializeField]
        protected PickUpEvent OnPickUp;
        [SerializeField]
        protected float rotationSpeed = 20f;
        [SerializeField]
        [Range(0f, 1f)] protected float rangeBronze = 0.6f;
        [SerializeField]
        [Range(0f, 1f)] protected float rangeSilver = 0.9f;

        protected int value;
        private Transform m_transform;

        private void Awake()
        {
            m_transform = GetComponent<Transform>();
            float random = UnityEngine.Random.Range(0f, 1f);
            Transform Cylinder = this.transform.GetChild(0).GetChild(0);
            Transform Cube = this.transform.GetChild(0).GetChild(1);

            if (random <= rangeBronze)
            {
                value = 10;
                Cylinder.GetComponent<MeshRenderer>().material = materialBronze;
                Cube.GetComponent<MeshRenderer>().material = materialBronze;
            }
            else
            {
                if (random <= rangeSilver)
                {
                    value = 20;
                    Cylinder.GetComponent<MeshRenderer>().material = materialSilver;
                    Cube.GetComponent<MeshRenderer>().material = materialSilver;
                }
                else
                {
                    value = 50;
                    Cylinder.GetComponent<MeshRenderer>().material = materialGold;
                    Cube.GetComponent<MeshRenderer>().material = materialGold;
                }
            }
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