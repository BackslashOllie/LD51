using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXAdjuster : MonoBehaviour
{
    public bool manuallyControlled = false;
    public bool manuallyRepeat = false;
    public float repeatTime = 2f;
    AudioSource audioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.Instance.GetSFXAudioLevel();
        if (!manuallyControlled)
        {
            AudioManager.SFXVolumeChanged += OnVolumeChanged;
            if (manuallyRepeat)
            {
                StartCoroutine(RepeatSFX());
            }
            else
            {
                audioSource.Play();
            }
        }
    }

    public void StartAudioSFX()
    {
        AudioManager.SFXVolumeChanged += OnVolumeChanged;
        if (manuallyRepeat)
        {
            StartCoroutine(RepeatSFX());
        }
        else
        {
            audioSource.Play();
        }
    }

    public void StopAudioSFX()
    {
        StopAllCoroutines();
        AudioManager.SFXVolumeChanged -= OnVolumeChanged;
    }

    private IEnumerator RepeatSFX()
    {
        audioSource.Play();
        yield return new WaitForSeconds(repeatTime);
        StartCoroutine(RepeatSFX());
    }

    private void OnVolumeChanged()
    {
        audioSource.volume = AudioManager.Instance.GetSFXAudioLevel();
    }

    void OnDisable()
    {
        StopAllCoroutines();
        AudioManager.SFXVolumeChanged -= OnVolumeChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
