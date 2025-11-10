using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMixerVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public bool audioOn = true;

    public void ToggleMixerVolume()
    {
        if (audioOn)
            mixer.SetFloat("MusicVolume", 0);
        else
            mixer.SetFloat("MusicVolume", -80);
        audioOn = !audioOn;
    }
    public void ToggleSFXVolume()
    {
        if (audioOn)
            mixer.SetFloat("SFXVolume", 0);
        else
            mixer.SetFloat("SFXVolume", -80);
        audioOn = !audioOn;
    }
}
