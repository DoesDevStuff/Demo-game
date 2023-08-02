using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Resource : MonoBehaviour
{
    [field: SerializeField]
    public SO_ResourceData ResourceData { get; set; }

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PickUpResources()
    {
        StartCoroutine(DestroyCoroutine());
    }

    /// <summary>
    /// when the player steps on the item, we are going to perform some checks. 
    /// Those are:
    /// - if player has enough ammo or health 
    /// - if in luck, and we need health or ammo && 
    /// we have actually stepped on the package that gives us those resources 
    /// 
    /// In that case we will first play a sound. 
    /// Then we are going to destroy the package when the sound stops.
    /// </summary>

    IEnumerator DestroyCoroutine()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
