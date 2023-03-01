using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PlayerScripts
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        protected float acceleration = 20f;
        [SerializeField][Tooltip("The higher the value of the mitigation, the less force will move your character while in the air when pressing movement keys.")]
        protected float airAccelerationMitigation = 5f;
        [SerializeField]
        protected float maxSpeed = 7f;
        [SerializeField]
        protected float jumpSpeed = 400f;
        [SerializeField]
        protected float maxRotationSpeed = 1f;
        [Tooltip("This determines the collision margin between the player and the environment. Lower numbers are stricter.")]
        protected float skinWidth = 0.01f;

        [SerializeField]
        private CinemachineVirtualCamera mainCamera;
        [SerializeField]
        private CinemachineVirtualCamera topDownCamera;
        [SerializeField]
        private CinemachineVirtualCamera highCamera;
        [SerializeField]
        private BoxCollider attackHitbox;
        [SerializeField]
        protected LayerMask groundLayers;

        private CinemachineVirtualCamera currentCamera;
        protected PlayerInput inputs;
        private Rigidbody m_rigidbody;
        private Animator m_animator;
        private CapsuleCollider m_collider;
        private Collider lastCollider;
        private bool isGrounded;
        private bool isHit;
        private bool isAttacking;

        void Awake()
        {
            inputs = GetComponent<PlayerInput>();
            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            m_collider = GetComponent<CapsuleCollider>();

            mainCamera.Priority = 10;
            currentCamera = mainCamera;
        }

        void FixedUpdate()
        {
            string[] currentInput = inputs.GetInput();

            if (lastCollider != null)
            {
                Vector3 positionCollider = lastCollider.ClosestPoint(new Vector3(transform.position.x, transform.position.y + m_collider.radius, transform.position.z));
                Vector3 m_positionVerification = new Vector3(transform.position.x, transform.position.y + m_collider.radius, transform.position.z);

                if (Physics.Raycast(m_positionVerification, positionCollider - m_positionVerification, m_collider.radius + skinWidth, groundLayers) && (transform.position.y + m_collider.radius) - positionCollider.y > 0)
                {
                    isGrounded = true;
                    m_animator.SetBool("isGrounded", isGrounded);
                }
                else
                {
                    isGrounded = false;
                    m_animator.SetBool("isGrounded", isGrounded);
                }
            }

            if (!isHit && !isAttacking)
            {
                if (currentInput[0] != "")
                {
                    if (currentInput[0] == "Forward")
                    {
                        if (isGrounded)
                        {
                            m_rigidbody.AddForce(new Vector3(currentCamera.transform.forward.x, 0, currentCamera.transform.forward.z).normalized * acceleration, ForceMode.Acceleration);
                        } else
                        {
                            m_rigidbody.AddForce(new Vector3(currentCamera.transform.forward.x, 0, currentCamera.transform.forward.z).normalized * acceleration / airAccelerationMitigation, ForceMode.Acceleration);
                        }
                    }
                    if (currentInput[0] == "Backward")
                    {
                        if (isGrounded)
                        {
                            m_rigidbody.AddForce(new Vector3(currentCamera.transform.forward.x, 0, currentCamera.transform.forward.z).normalized * -acceleration, ForceMode.Acceleration);
                        } else
                        {
                            m_rigidbody.AddForce(new Vector3(currentCamera.transform.forward.x, 0, currentCamera.transform.forward.z).normalized * -acceleration / airAccelerationMitigation, ForceMode.Acceleration);
                        }
                    }
                }
                if (currentInput[1] != "")
                {
                    if (currentInput[1] == "Left")
                    {
                        if (isGrounded)
                        {
                            m_rigidbody.AddForce(currentCamera.transform.right * -acceleration, ForceMode.Acceleration);
                        } else
                        {
                            m_rigidbody.AddForce(currentCamera.transform.right * -acceleration / airAccelerationMitigation, ForceMode.Acceleration);
                        }
                    }
                    if (currentInput[1] == "Right")
                    {
                        if (isGrounded)
                        {
                            m_rigidbody.AddForce(currentCamera.transform.right * acceleration, ForceMode.Acceleration);
                        } else
                        {
                            m_rigidbody.AddForce(currentCamera.transform.right * acceleration / airAccelerationMitigation, ForceMode.Acceleration);
                        }
                    }
                }
                if (currentInput[2] == "Jump" && isGrounded)
                {
                    m_rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
                }

                if (currentInput[3] != "")
                {
                    if (currentInput[3] == "Look Up")
                    {
                        highCamera.Priority = 11;
                        currentCamera = highCamera;
                    }
                    if (currentInput[3] == "Look Down")
                    {
                        topDownCamera.Priority = 11;
                        currentCamera = topDownCamera;
                    }
                }
                else
                {
                    topDownCamera.Priority = 0;
                    highCamera.Priority = 0;
                    currentCamera = mainCamera;
                }

                if (currentInput[4] == "Attack")
                {
                    StartCoroutine(Attack());
                }
            }

            if (isGrounded && m_rigidbody.velocity.magnitude > maxSpeed)
                m_rigidbody.velocity = m_rigidbody.velocity.normalized * maxSpeed;

            if (m_rigidbody.velocity.magnitude > 0.01)
                m_rigidbody.rotation = Quaternion.RotateTowards(m_rigidbody.rotation, Quaternion.LookRotation(new Vector3(m_rigidbody.velocity.x, 0, m_rigidbody.velocity.z)), maxRotationSpeed);

            m_animator.SetFloat("velocity_player", m_rigidbody.velocity.magnitude);
        }

        private void OnTriggerStay(Collider other)
        {
            lastCollider = other;
        }

        public IEnumerator PushPlayer(Vector3 force)
        {
            if (!isHit)
            {
                m_rigidbody.velocity = force;
                isHit = true;
                m_animator.SetBool("isHit", isHit);

                yield return new WaitForSeconds(1.5f);
                yield return new WaitUntil(() => isGrounded);
                yield return new WaitForSeconds(0.5f);

                isHit = false;
                m_animator.SetBool("isHit", isHit);
            }
        }

        private IEnumerator Attack()
        {
            if (!isHit && !isAttacking)
            {
                isAttacking = true;
                m_animator.SetBool("isAttacking", true);
                attackHitbox.enabled = true;

                yield return new WaitForSeconds(1f);

                isAttacking = false;
                m_animator.SetBool("isAttacking", false);
                attackHitbox.enabled = false;
            }
        }
    }
}