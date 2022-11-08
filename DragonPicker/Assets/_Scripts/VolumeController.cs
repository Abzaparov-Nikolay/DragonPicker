using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetMasterValue(float slideValue)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(slideValue)*20);
    }

    public void SetBackgroundValue(float slideValue)
    {
        audioMixer.SetFloat("BackgroundVol", Mathf.Log10(slideValue)*20);
    }

    public void SetSFXValue(float slideValue)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(slideValue)*20);
    }
}
