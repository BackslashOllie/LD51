using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SFXAdjuster))]
public class BrokenToiletEvent : GameEvent
{
    public SFXAdjuster sfxAdjuster;
    public ParticleSystem water;
    public GameObject wetFloorSign;
    public override void StartEvent(float time)
    {
        sfxAdjuster = GetComponent<SFXAdjuster>();
        base.StartEvent(time);
        water.Play();
        sfxAdjuster.StartAudioSFX();
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        sfxAdjuster.StopAudioSFX();
        wetFloorSign.transform.position = transform.position + (transform.forward * 3);
        wetFloorSign.transform.Rotate(0, Random.Range(0,360), 0);
        water.Stop();
    }
}
