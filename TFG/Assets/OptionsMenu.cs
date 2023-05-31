using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider backMusicSlider, SFXSlider;

    private void Start()
    {
        PlayerPrefs.SetFloat("musicVol", backMusicSlider.value);
        PlayerPrefs.SetFloat("sfxVol", SFXSlider.value);
    }

    public void RegulateMusicVolume()
    {
        AudioManager.Instance.ChangeMusicVolume(backMusicSlider.value);
    }

    public void RegulateSFXVolume()
    {
        AudioManager.Instance.ChangeSFXVolume(SFXSlider.value);
    }

    public void ToggleFullScreen(bool setFullScreen)
    {
        Screen.fullScreen = setFullScreen;
    }
}
