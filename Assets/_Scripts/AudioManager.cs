using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{

    private string masterVolumeRef = "MasterVolumeKey";
    private string musicVolumeRef = "MusicVolumeKey";
    private string sfxVolumeRef = "SFXVolumeKey";

    public float defaultMasterVolume = 1f;
    public float defaultMusicVolume = .5f;
    public float defaultSFXVolume = .75f;
    public float timeBetweenTracks = 1f;

    public AudioClip[] musicPlayList;
    public AudioSource musicSource;

    public static Action SFXVolumeChanged;

    private void Start()
    {
        AdjustVolume(GetMasterAudioLevel() * GetMusicAudioLevel());
        PlayRandomTrack();
    }
    public float GetMasterAudioLevel()
    {
        return PlayerPrefs.GetFloat(masterVolumeRef, defaultMasterVolume);
    }

    public void SetMasterAudioLevel(float newVolume)
    {
        PlayerPrefs.SetFloat(masterVolumeRef, newVolume);
        AdjustVolume(newVolume*GetMusicAudioLevel());
        SFXVolumeChanged?.Invoke();
    }

    public float GetSFXAudioLevel()
    {
        return PlayerPrefs.GetFloat(sfxVolumeRef, defaultSFXVolume) * GetMasterAudioLevel();
    }

    public void AdjustVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }

    public void PlayRandomTrack()
    {
        StopAllCoroutines();
        var trackToPlay = UnityEngine.Random.Range(0, musicPlayList.Length);
        musicSource.Stop();
        StartCoroutine(DelayAndPlay(trackToPlay));
    }

    private IEnumerator DelayAndPlay(int trackToPLay)
    {
        musicSource.clip = musicPlayList[trackToPLay];
        yield return new WaitForSeconds(timeBetweenTracks);
        musicSource.Play();
        yield return new WaitForSeconds(musicPlayList[trackToPLay].length);
        PlayRandomTrack();
    }

    public void SetSFXVolumeLevel(float newVolume)
    {
        PlayerPrefs.SetFloat(sfxVolumeRef, newVolume);
        SFXVolumeChanged?.Invoke();
    }

    public float GetMusicAudioLevel()
    {
        return PlayerPrefs.GetFloat(musicVolumeRef, defaultMusicVolume);
    }

    public void SetMusicVolumeLevel(float newVolume)
    {
        PlayerPrefs.SetFloat(musicVolumeRef, newVolume);
        AdjustVolume(newVolume * GetMasterAudioLevel());
    }


}
