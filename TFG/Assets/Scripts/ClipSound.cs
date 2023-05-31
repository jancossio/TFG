using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class ClipSound 
{
    public AudioClip clip;
    public string soundName;

    private AudioSource soundSource;

    public void SetSoundSource(AudioSource newSource)
    {
        soundSource = newSource;
    }
}
