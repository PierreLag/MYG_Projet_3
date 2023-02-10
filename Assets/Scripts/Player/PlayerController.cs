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
        protected float jumpSpeed = 400f;

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
                    m_rigidbody.AddForce(Vector3.forward * acceleration, ForceMode.Acceleration);
                }
                if (currentInput[0] == "Backward")
                {
                    m_rigidbody.AddForce(Vector3.back * acceleration, ForceMode.Acceleration);
                }
            }
            if (currentInput[1] != "")
            {
                if (currentInput[1] == "Left")
                {
                    m_rigidbody.AddForce(Vector3.left * acceleration, ForceMode.Acceleration);
                }
                if (currentInput[1] == "Right")
                {
                    m_rigidbody.AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
                }
            }
            if (currentInput[2] != "")
            {
                if (currentInput[2] == "Jump" && Mathf.MoveTowards(m_rigidbody.velocity.y, 0, 0.001f) == 0)
                {
                    m_rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
                }
            }

            m_animator.SetFloat("velocity_player", m_rigidbody.velocity.magnitude);
        }
    }
}