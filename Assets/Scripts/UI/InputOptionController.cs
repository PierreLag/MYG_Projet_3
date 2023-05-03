using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

namespace CustomUI
{
    public class InputOptionController : MonoBehaviour
    {
        [SerializeField]
        protected Button buttonForward;
        [SerializeField]
        protected Button buttonBackward;
        [SerializeField]
        protected Button buttonLeft;
        [SerializeField]
        protected Button buttonRight;
        [SerializeField]
        protected Button buttonJump;
        [SerializeField]
        protected Button buttonAttack;
        [SerializeField]
        protected Button buttonPause;

        [SerializeField]
        protected RawImage keyInputscreen;

        private KeyCode keybinding;
        private bool isAwaitingInput;

        // Start is called before the first frame update
        void Start()
        {
            ResetDisplay();
            isAwaitingInput = false;
        }

        private void OnGUI()
        {
            Event e = Event.current;

            if (isAwaitingInput && keybinding == KeyCode.None && (e.isKey || e.isMouse))
            {
                if (e.isKey)
                    keybinding = e.keyCode;
                else
                    keybinding = Enum.Parse<KeyCode>("Mouse" + e.button);
            }
        }

        private void ResetDisplay()
        {
            buttonForward.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Forward");
            buttonBackward.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Backward");
            buttonLeft.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Left");
            buttonRight.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Right");
            buttonJump.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Jump");
            buttonAttack.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Attack");
            buttonPause.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("Pause");
        }

        public void IsEditing(Button button)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "...";
        }

        private void AwaitKeybind()
        {
            isAwaitingInput = true;
            keybinding = KeyCode.None;

            while (keybinding == KeyCode.None);

            isAwaitingInput = false;
        }

        public async void ChangeKey(string keyFunction)
        {
            buttonForward.interactable = false;
            buttonBackward.interactable = false;
            buttonLeft.interactable = false;
            buttonRight.interactable = false;
            buttonJump.interactable = false;
            buttonAttack.interactable = false;
            buttonPause.interactable = false;

            keyInputscreen.gameObject.SetActive(true);

            Task keybindingTask = Task.Run(() => AwaitKeybind());

            await keybindingTask;

            keyInputscreen.gameObject.SetActive(false);

            PlayerPrefs.SetString(keyFunction, keybinding.ToString());
            PlayerPrefs.Save();

            buttonForward.interactable = true;
            buttonBackward.interactable = true;
            buttonLeft.interactable = true;
            buttonRight.interactable = true;
            buttonJump.interactable = true;
            buttonAttack.interactable = true;
            buttonPause.interactable = true;

            ResetDisplay();
        }
    }
}