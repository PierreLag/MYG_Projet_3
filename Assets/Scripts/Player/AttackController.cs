using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacles;

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