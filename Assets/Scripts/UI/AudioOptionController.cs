using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class AudioOptionController : MonoBehaviour
    {
        [SerializeField]
        private float defaultMusicVolume = 1f;
        [SerializeField]
        private float defaultSFXVolume = 1f;

        [SerializeField]
        protected Slider musicSlider;
        [SerializeField]
        protected Slider sfxSlider;

        protected static float musiqueVolume;
        protected static float sfxVolume;

        // Start is called before the first frame update
        void Start()
        {
            if (!PlayerPrefs.HasKey("MusiqueVolume"))
            {
                PlayerPrefs.SetFloat("MusiqueVolume", defaultMusicVolume);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey("SFXVolume"))
            {
                PlayerPrefs.SetFloat("SFXVolume", defaultSFXVolume);
                PlayerPrefs.Save();
            }

            musicSlider.value = PlayerPrefs.GetFloat("MusiqueVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        public void ChangeMusicVolume(float value)
        {
            PlayerPrefs.SetFloat("MusiqueVolume", value);
            PlayerPrefs.Save();
        }

        public void ChangeSFXVolume(float value)
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            PlayerPrefs.Save();
        }
    }
}