using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        protected KeyCode defaultKeyForward = KeyCode.Z;
        [SerializeField]
        protected KeyCode defaultKeyBackward = KeyCode.S;
        [SerializeField]
        protected KeyCode defaultKeyLeft = KeyCode.Q;
        [SerializeField]
        protected KeyCode defaultKeyRight = KeyCode.D;
        [SerializeField]
        protected KeyCode defaultKeyJump = KeyCode.Space;
        [SerializeField]
        protected KeyCode defaultKeyAttack = KeyCode.Mouse0;
        [SerializeField]
        protected KeyCode defaultKeyPause = KeyCode.Escape;

        void Awake()
        {
            if (!PlayerPrefs.HasKey("Forward"))
            {
                PlayerPrefs.SetString("Forward", defaultKeyForward.ToString());
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Backward"))
            {
                PlayerPrefs.SetString("Backward", defaultKeyBackward.ToString());
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Left"))
            {
                PlayerPrefs.SetString("Left", defaultKeyLeft.ToString());
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Right"))
            {
                PlayerPrefs.SetString("Right", defaultKeyRight.ToString());
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Jump"))
            {
                PlayerPrefs.SetString("Jump", defaultKeyJump.ToString());
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Attack"))
            {
                PlayerPrefs.SetString("Attack", defaultKeyAttack.ToString());
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("Pause"))
            {
                PlayerPrefs.SetString("Pause", defaultKeyPause.ToString());
                PlayerPrefs.Save();
            }
        }

        public string[] GetInput()
        {
            string[] input = { "", "", "", "" };

            if (Input.GetKey(Enum.Parse<KeyCode>(PlayerPrefs.GetString("Forward"))))
            {
                input[0] = "Forward";
            }
            if (Input.GetKey(Enum.Parse<KeyCode>(PlayerPrefs.GetString("Backward"))))
            {
                if (input[0] == "Forward")
                    input[0] = "";
                else
                    input[0] = "Backward";
            }

            if (Input.GetKey(Enum.Parse<KeyCode>(PlayerPrefs.GetString("Left"))))
            {
                input[1] = "Left";
            }
            if (Input.GetKey(Enum.Parse<KeyCode>(PlayerPrefs.GetString("Right"))))
            {
                if (input[1] == "Left")
                    input[1] = "";
                else
                    input[1] = "Right";
            }

            if (Input.GetKey(Enum.Parse<KeyCode>(PlayerPrefs.GetString("Jump"))))
            {
                input[2] = "Jump";
            }

            if (Input.GetKey(Enum.Parse<KeyCode>(PlayerPrefs.GetString("Attack"))))
            {
                input[3] = "Attack";
            }

            return input;
        }
    }
}