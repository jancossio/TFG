using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public ClipSound[] soundSFX, backMusic;

    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource MusicSource;

    // Start is called before the first frame update
    void Awake()
    {
        MusicSource.volume = PlayerPrefs.GetFloat("musicVol", 0.5f);
        SFXSource.volume = PlayerPrefs.GetFloat("sfxVol", 0.75f);

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        PlayMusicTrack("LevelSong");
    }

    public void PlaySoundEffect(string effectName)
    {
        ClipSound s = Array.Find<ClipSound>(soundSFX, song => song.soundName == effectName);

        if(s != null)
        {
            SFXSource.PlayOneShot(s.clip);
        }
        else
        {
            //Debug.Log("Error!!: Sound effect not found!!");
        }
    }

    public void PlayMusicTrack(string songName)
    {
        ClipSound c = Array.Find<ClipSound>(backMusic, effect => effect.soundName == songName);

        if (c != null)
        {
            MusicSource.clip = c.clip;
            MusicSource.Play();
        }
        else
        {
            //Debug.Log("Error!!: Music track not found!!");
        }
    }

    public void StopMusicTrack()
    {
        MusicSource.Stop();
    }

    public void PauseMusicTrack(bool paused)
    {
        if (paused)
        {
            MusicSource.Pause();
        }
        else
        {
            MusicSource.UnPause();
        }
    }

    public void ChangeMusicVolume(float newVolume)
    {
        newVolume = Mathf.Clamp(newVolume, 0f, 1f);
        PlayerPrefs.SetFloat("musicVol", newVolume);
        MusicSource.volume = newVolume;
    }

    public void ChangeSFXVolume(float newVolume)
    {
        newVolume = Mathf.Clamp(newVolume, 0f, 1f);
        PlayerPrefs.SetFloat("sfxVol", newVolume);
        SFXSource.volume = newVolume;
    }
}
