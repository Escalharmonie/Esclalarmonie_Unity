using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    #region Parameters

    [SerializeField] private AudioMixer MainMixer;

    #endregion

    public void SetLowpassCutoff(float value)
    {
        value = Mathf.Clamp(value, 10f, 22_000f);

        MainMixer.SetFloat("Lowpass", value);
    }
    
    public void SetHighpassCutoff(float value)
    {
        value = Mathf.Clamp(value, 10f, 22_000f);

        MainMixer.SetFloat("Highpass", value);
    }
    
    public void SetDistortionValue(float value)
    {
        value = Mathf.Clamp01(value);

        MainMixer.SetFloat("Distortion", value);
    }
}
