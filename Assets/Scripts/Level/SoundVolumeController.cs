using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level {
    [RequireComponent(typeof(AudioSource))]
    public class SoundVolumeController : MonoBehaviour
    {
        [SerializeField]
        private SoundType soundType = SoundType.SFX;
        [SerializeField] [Range(0f, 1f)]
        protected float volumeMultiplier = 1f;

        protected AudioSource m_audioSource;

        private enum SoundType
        {
            MUSIC,
            SFX
        }

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 0 && m_audioSource.isPlaying)
            {
                m_audioSource.Pause();
            }
            if (Time.timeScale == 1 && !m_audioSource.isPlaying)
            {
                m_audioSource.UnPause();
            }

            if (soundType == SoundType.MUSIC)
                m_audioSource.volume = PlayerPrefs.GetFloat("MusiqueVolume") * volumeMultiplier;
            if (soundType == SoundType.SFX)
                m_audioSource.volume = PlayerPrefs.GetFloat("SFXVolume") * volumeMultiplier;
        }
    }
}