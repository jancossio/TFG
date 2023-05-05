using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public AudioSource[] soundSFX;

    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource MusicSource;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance = null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Más de un GameManager en escena");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect(AudioClip effectClip)
    {
        SFXSource.clip = effectClip;
        SFXSource.Play();
    }

    public void PlayMusicTrack(AudioClip musicClip)
    {
        MusicSource.clip = musicClip;
        MusicSource.Play();
    }

}
