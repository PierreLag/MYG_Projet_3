using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace CustomUI
{
    public class VideoOptionController : MonoBehaviour
    {
        [SerializeField]
        protected PostProcessProfile sharedProfile;

        [SerializeField]
        protected Toggle vignetteToggle;
        [SerializeField]
        protected Toggle bloomToggle;
        [SerializeField]
        protected Toggle chromaticAberrationToggle;
        [SerializeField]
        protected Toggle dofToggle;
        [SerializeField]
        protected Toggle grainToggle;
        [SerializeField]
        protected Toggle lensDistortionToggle;
        [SerializeField]
        protected Toggle colorGradingToggle;

        public bool GetVignetteState()
        {
            sharedProfile.TryGetSettings<Vignette>(out Vignette vignette);
            return vignette.active;
        }

        public void SetVignetteState(bool state)
        {
            sharedProfile.TryGetSettings<Vignette>(out Vignette vignette);
            vignette.active = state;
        }

        public bool GetBloomState()
        {
            sharedProfile.TryGetSettings<Bloom>(out Bloom bloom);
            return bloom.active;
        }

        public void SetBloomState(bool state)
        {
            sharedProfile.TryGetSettings<Bloom>(out Bloom bloom);
            bloom.active = state;
        }

        public bool GetChromaticAberrationState()
        {
            sharedProfile.TryGetSettings<ChromaticAberration>(out ChromaticAberration ca);
            return ca.active;
        }

        public void SetChromaticAberrationState(bool state)
        {
            sharedProfile.TryGetSettings<ChromaticAberration>(out ChromaticAberration ca);
            ca.active = state;
        }

        public bool GetDOFState()
        {
            sharedProfile.TryGetSettings<DepthOfField>(out DepthOfField dof);
            return dof.active;
        }

        public void SetDOFState(bool state)
        {
            sharedProfile.TryGetSettings<DepthOfField>(out DepthOfField dof);
            dof.active = state;
        }

        public bool GetGrainState()
        {
            sharedProfile.TryGetSettings<Grain>(out Grain grain);
            return grain.active;
        }

        public void SetGrainState(bool state)
        {
            sharedProfile.TryGetSettings<Grain>(out Grain grain);
            grain.active = state;
        }

        public bool GetLensDistortionState()
        {
            sharedProfile.TryGetSettings<LensDistortion>(out LensDistortion ld);
            return ld.active;
        }

        public void SetLensDistortionState(bool state)
        {
            sharedProfile.TryGetSettings<LensDistortion>(out LensDistortion ld);
            ld.active = state;
        }

        public bool GetColorGradingState()
        {
            sharedProfile.TryGetSettings<ColorGrading>(out ColorGrading cg);
            return cg.active;
        }

        public void SetColorGradingState(bool state)
        {
            sharedProfile.TryGetSettings<ColorGrading>(out ColorGrading cg);
            cg.active = state;
        }

        private void Start()
        {
            vignetteToggle.isOn = GetVignetteState();
            bloomToggle.isOn = GetBloomState();
            chromaticAberrationToggle.isOn = GetChromaticAberrationState();
            dofToggle.isOn = GetDOFState();
            grainToggle.isOn = GetGrainState();
            lensDistortionToggle.isOn = GetLensDistortionState();
            colorGradingToggle.isOn = GetColorGradingState();
        }
    }
}