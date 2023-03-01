using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Interactables
{
    public class Pushable : MonoBehaviour
    {
        [SerializeField]
        protected float pushForce = 100f;

        private Rigidbody m_rigidbody;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        public IEnumerator GetPushed(Collider source)
        {
            m_rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            if (Mathf.Abs(source.transform.forward.x) >= Mathf.Abs(source.transform.forward.z))
            {
                m_rigidbody.AddForce(new Vector3(source.transform.forward.x, 0, 0).normalized * pushForce, ForceMode.Force);
            }
            else
            {
                m_rigidbody.AddForce(new Vector3(0, 0, source.transform.forward.z).normalized * pushForce, ForceMode.Force);
            }

            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => m_rigidbody.velocity.magnitude <= 0.0001);

            m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x), transform.localPosition.y, Mathf.Round(transform.localPosition.z));
        }
    }
}