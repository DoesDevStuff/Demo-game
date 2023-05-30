using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWalkAudio : AudioPlayer
{
    
    [SerializeField]
    protected AudioClip soundClip;
    
    public void PlayFootSteps()
    {
        PlayClipWithRandomnisedPitch(soundClip);
    }
}
