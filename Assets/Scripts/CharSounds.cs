using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherit from Audio Player    
public class CharSounds : AudioPlayer, IDamageHandler, IDeathHandler
{
    [SerializeField]
    private AudioClip _hitFeedbackClip = null, _deathFeedbackClip = null, _detectedVoiceFeedbackClip = null;
    
    public void OnDamage(int damage, GameObject givesDamage)
    {
        PlayClipWithRandomnisedPitch(_hitFeedbackClip);
    }
     
    public void OnDeath()
    {
        PlaySoundClip(_deathFeedbackClip);
    }

    public void PlayDetectedVoiceSound()
    {
        PlayClipWithRandomnisedPitch(_detectedVoiceFeedbackClip);
    }
}
