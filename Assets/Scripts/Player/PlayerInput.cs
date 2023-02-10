using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        protected KeyCode keyForward = KeyCode.Z;
        [SerializeField]
        protected KeyCode keyBackward = KeyCode.S;
        [SerializeField]
        protected KeyCode keyLeft = KeyCode.Q;
        [SerializeField]
        protected KeyCode keyRight = KeyCode.D;
        [SerializeField]
        protected KeyCode keyJump = KeyCode.Space;

        public string[] GetInput()
        {
            string[] input = { "", "", "" };

            if (Input.GetKey(keyForward))
            {
                input[0] = "Forward";
            }
            if (Input.GetKey(keyBackward))
            {
                if (input[0] == "Forward")
                    input[0] = "";
                else
                    input[0] = "Backward";
            }

            if (Input.GetKey(keyLeft))
            {
                input[1] = "Left";
            }
            if (Input.GetKey(keyRight))
            {
                if (input[1] == "Left")
                    input[1] = "";
                else
                    input[1] = "Right";
            }

            if (Input.GetKey(keyJump))
            {
                input[2] = "Jump";
            }

            return input;
        }
    }
}