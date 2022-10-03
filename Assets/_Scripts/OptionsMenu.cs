using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;


    void OnEnable()
    {
        masterVolumeSlider.value = AudioManager.Instance.GetMasterAudioLevel();
        musicVolumeSlider.value = AudioManager.Instance.GetMusicAudioLevel();
        sfxVolumeSlider.value = AudioManager.Instance.GetSFXAudioLevel();
    }

    public void SetMasterVolume()
    {
        AudioManager.Instance.SetMasterAudioLevel(masterVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.Instance.SetSFXVolumeLevel(sfxVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.SetMusicVolumeLevel(musicVolumeSlider.value);
    }

}
