using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private Transform mainCameraTransform;

        protected PlayerInput inputs;
        private Rigidbody m_rigidbody;
        private Animator m_animator;

        void Awake()
        {
            inputs = GetComponent<PlayerInput>();
            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            string[] currentInput = inputs.GetInput();
            if (currentInput[0] != "")
            {
                if (currentInput[0] == "Forward")
                {
                    m_rigidbody.AddForce(mainCameraTransform.forward * acceleration, ForceMode.Acceleration);
                }
                if (currentInput[0] == "Backward")
                {
                    m_rigidbody.AddForce(mainCameraTransform.forward * -acceleration, ForceMode.Acceleration);
                }
            }
            if (currentInput[1] != "")
            {
                if (currentInput[1] == "Left")
                {
                    m_rigidbody.AddForce(mainCameraTransform.right * -acceleration, ForceMode.Acceleration);
                }
                if (currentInput[1] == "Right")
                {
                    m_rigidbody.AddForce(mainCameraTransform.right * acceleration, ForceMode.Acceleration);
                }
            }
            if (currentInput[2] != "")
            {
                if (currentInput[2] == "Jump" && Mathf.MoveTowards(m_rigidbody.velocity.y, 0, 0.001f) == 0)
                {
                    m_rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
                }
            }
            if (m_rigidbody.velocity.magnitude > maxSpeed)
                m_rigidbody.velocity = m_rigidbody.velocity.normalized * maxSpeed;

            if (m_rigidbody.velocity.magnitude > 0.01)
                m_rigidbody.rotation = Quaternion.RotateTowards(m_rigidbody.rotation, Quaternion.LookRotation(new Vector3(m_rigidbody.velocity.x, 0, m_rigidbody.velocity.z)), maxRotationSpeed);

            m_animator.SetFloat("velocity_player", m_rigidbody.velocity.magnitude);
        }
    }
}