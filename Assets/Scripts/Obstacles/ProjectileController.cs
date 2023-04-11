using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace Obstacles
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        protected LayerMask destroyOnLayerImpact;
        [SerializeField]
        protected float skinWidth = 1f;
        [SerializeField]
        protected float pushForce = 500f;
        [SerializeField]
        protected float pushHeight = 10f;

        void OnCollisionEnter(Collision other)
        {
            Vector3 positionCollider = other.transform.position;
            if (Physics.Raycast(transform.position, positionCollider - transform.position, skinWidth, destroyOnLayerImpact))
            {
                Destroy(gameObject);
            }
        }

        public void Disappear()
        {
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}