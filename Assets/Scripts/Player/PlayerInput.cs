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
        [SerializeField]
        protected KeyCode keyAttack = KeyCode.Mouse0;
        [SerializeField]
        protected KeyCode keyPause = KeyCode.Escape;

        protected Dictionary<string, KeyCode> keyMap;

        PlayerInput()
        {
            keyMap = new Dictionary<string, KeyCode>();
            keyMap.Add("Forward", keyForward);
            keyMap.Add("Backward", keyBackward);
            keyMap.Add("Left", keyLeft);
            keyMap.Add("Right", keyRight);
            keyMap.Add("Jump", keyJump);

            keyMap.Add("Attack", keyAttack);

            keyMap.Add("Pause", keyPause);
        }

        public Dictionary<string, KeyCode> GetKeyMapping()
        {
            return keyMap;
        }

        public string[] GetInput()
        {
            string[] input = { "", "", "", "" };

            if (Input.GetKey(keyMap["Forward"]))
            {
                input[0] = "Forward";
            }
            if (Input.GetKey(keyMap["Backward"]))
            {
                if (input[0] == "Forward")
                    input[0] = "";
                else
                    input[0] = "Backward";
            }

            if (Input.GetKey(keyMap["Left"]))
            {
                input[1] = "Left";
            }
            if (Input.GetKey(keyMap["Right"]))
            {
                if (input[1] == "Left")
                    input[1] = "";
                else
                    input[1] = "Right";
            }

            if (Input.GetKey(keyMap["Jump"]))
            {
                input[2] = "Jump";
            }

            if (Input.GetKey(keyMap["Attack"]))
            {
                input[3] = "Attack";
            }

            return input;
        }
    }
}