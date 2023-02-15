using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace Obstacles
{
    public class Pusher : MonoBehaviour
    {
        [SerializeField]
        protected float pushForce = 500f;
        [SerializeField]
        protected float pushHeight = 10f;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                Vector3 force = player.transform.position - GetComponent<Collider>().ClosestPoint(player.transform.position);
                force.y = 0;
                force = force.normalized * pushForce;
                force.y = pushHeight;
                StartCoroutine(player.PushPlayer(force));
            }
        }
    }
}