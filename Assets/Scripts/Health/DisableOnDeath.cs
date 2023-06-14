using System.Collections;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour, IDeathHandler
{
    public Collider2D _collider;
    public SpriteRenderer _renderer;
    public float _destroyWaitTime = 0.2f;

    public void OnDeath()
    {
        if(_collider)
            _collider.enabled = false;
        if(_renderer)
            _renderer.enabled = false;

        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_destroyWaitTime);
        Destroy(gameObject);
    }
}
