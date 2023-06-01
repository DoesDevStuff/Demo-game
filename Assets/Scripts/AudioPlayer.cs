using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class AudioPlayer : MonoBehaviour
{
    protected AudioSource soundClipSource;

    [SerializeField]
    protected float randomiseAudioPitch = 0.06f;
    protected float basePitch;
    
    private void Awake()
    {
        soundClipSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        basePitch = soundClipSource.pitch;
    }

    protected void PlayClipWithRandomnisedPitch(AudioClip _clip)
    {
        var randomPitch = UnityEngine.Random.Range(-randomiseAudioPitch, randomiseAudioPitch);
        soundClipSource.pitch = basePitch + randomPitch;
        PlaySoundClip(_clip);
    }

    protected void PlaySoundClip(AudioClip _clip)
    {
        soundClipSource.Stop();
        soundClipSource.clip = _clip;
        soundClipSource.Play();
    }

    
}
