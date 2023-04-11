using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject projectile;
        [SerializeField]
        protected float attackDelay = 2f;
        [SerializeField]
        protected float attackDelayOffset = 0f;
        [SerializeField]
        protected float attackForce = 10f;

        protected float attackTimer;

        private void Awake()
        {
            attackTimer = attackDelayOffset;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            attackTimer += Time.fixedDeltaTime;
            if (attackTimer >= attackDelay)
            {
                Shoot();
                attackTimer -= attackDelay;
            }
        }

        private void Shoot()
        {
            GameObject newProjectile = Instantiate(projectile, transform);
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * attackForce);
        }
    }
}