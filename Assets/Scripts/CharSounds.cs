using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherit from Audio Player    
public class CharSounds : AudioPlayer
{
    [SerializeField]
    private AudioClip _hitFeedbackClip = null, _deathFeedbackClip = null, _detectedVoiceFeedbackClip = null;

    public void PlayHitSound()
    {
        PlayClipWithRandomnisedPitch(_hitFeedbackClip);
    }
     
    public void PlayDeathSound()
    {
        PlaySoundClip(_deathFeedbackClip);
    }

    public void PlayDetectedVoiceSound()
    {
        PlayClipWithRandomnisedPitch(_detectedVoiceFeedbackClip);
    }
}
