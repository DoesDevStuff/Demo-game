using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    [SerializeField]
    private AudioClip _shootingBulletsClip = null, _outOfBulletsClip = null;

    public void PlayShootBulletSound()
    {
        PlaySoundClip(_shootingBulletsClip);
    }

    public void PlayOutOfBulletSound()
    {
        PlaySoundClip(_outOfBulletsClip);
    }
}
