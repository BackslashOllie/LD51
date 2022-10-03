using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbEvent : GameEvent
{
    public Animator anim;
    public ParticleSystem sparks;
    public SFXAdjuster adjuster;
    
    public override void StartEvent(float time)
    {
        base.StartEvent(time);
        adjuster.StartAudioSFX();
        sparks.Play();
        anim.SetBool("Flashing", true);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        adjuster.StopAudioSFX();
        sparks.Stop();
        anim.SetBool("Flashing", false);
        anim.SetBool("Powered", true);
    }
    
    public override void FailEvent()
    {
        base.FailEvent();
        adjuster.StopAudioSFX();
        sparks.Stop();
        anim.SetBool("Flashing", false);
        anim.SetBool("Powered", false);
    }
}
