using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

namespace PlayerScripts {
    public class AttackController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Pushable>(out Pushable pushable))
            {
                StartCoroutine(pushable.GetPushed(GetComponent<Collider>()));
            }
        }
    }
}