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
        [SerializeField]
        protected float maxSpeed = 7f;
        [SerializeField]
        protected float jumpSpeed = 400f;
        [SerializeField]
        protected float maxRotationSpeed = 1f;

        [SerializeField]
        private CinemachineVirtualCamera mainCamera;
        [SerializeField]
        private CinemachineVirtualCamera topDownCamera;
        [SerializeField]
        private CinemachineVirtualCamera highCamera;

        private CinemachineVirtualCamera currentCamera;
        protected PlayerInput inputs;
        private Rigidbody m_rigidbody;
        private Animator m_animator;
        private CapsuleCollider m_collider;
        private bool isGrounded;
        private bool isHit;

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

            if (!isHit)
            {
                if (currentInput[0] != "")
                {
                    if (currentInput[0] == "Forward")
                    {
                        m_rigidbody.AddForce(new Vector3(currentCamera.transform.forward.x, 0, currentCamera.transform.forward.z).normalized * acceleration, ForceMode.Acceleration);
                    }
                    if (currentInput[0] == "Backward")
                    {
                        m_rigidbody.AddForce(new Vector3(currentCamera.transform.forward.x, 0, currentCamera.transform.forward.z).normalized * -acceleration, ForceMode.Acceleration);
                    }
                }
                if (currentInput[1] != "")
                {
                    if (currentInput[1] == "Left")
                    {
                        m_rigidbody.AddForce(currentCamera.transform.right * -acceleration, ForceMode.Acceleration);
                    }
                    if (currentInput[1] == "Right")
                    {
                        m_rigidbody.AddForce(currentCamera.transform.right * acceleration, ForceMode.Acceleration);
                    }
                }
                if (currentInput[2] != "")
                {
                    if (currentInput[2] == "Jump" && isGrounded)
                    {
                        m_rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
                    }
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
            }

            if (isGrounded && m_rigidbody.velocity.magnitude > maxSpeed)
                m_rigidbody.velocity = m_rigidbody.velocity.normalized * maxSpeed;

            if (m_rigidbody.velocity.magnitude > 0.01)
                m_rigidbody.rotation = Quaternion.RotateTowards(m_rigidbody.rotation, Quaternion.LookRotation(new Vector3(m_rigidbody.velocity.x, 0, m_rigidbody.velocity.z)), maxRotationSpeed);

            m_animator.SetFloat("velocity_player", m_rigidbody.velocity.magnitude);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 6)    // Level layer
            {
                if (m_collider.ClosestPoint(other.ClosestPoint(transform.position)).y < transform.position.y + m_collider.radius)
                {
                    isGrounded = true;
                    m_animator.SetBool("isGrounded", isGrounded);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 6)    // Level layer
            {
                if (m_collider.ClosestPoint(other.ClosestPoint(transform.position)).y < transform.position.y + m_collider.radius)
                    isGrounded = false;
                m_animator.SetBool("isGrounded", isGrounded);
            }
        }

        public IEnumerator PushPlayer(Vector3 force)
        {
            m_rigidbody.AddForce(force, ForceMode.Force);
            isHit = true;
            m_animator.SetBool("isHit", isHit);

            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => isGrounded);

            isHit = false;
            m_animator.SetBool("isHit", isHit);
        }
    }
}