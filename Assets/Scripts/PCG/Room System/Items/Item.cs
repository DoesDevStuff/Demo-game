using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private BoxCollider2D _itemCollider;

    [SerializeField]
    int health = 3;
    [SerializeField]
    bool nonDestructible;

    [SerializeField]
    private GameObject _hitFeedback, _destoyFeedback;

    public UnityEvent OnGetHit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Initialize(ItemData itemData)
    {
        //set sprite
        _spriteRenderer.sprite = itemData.sprite;
        //set sprite offset
        _spriteRenderer.transform.localPosition = new Vector2(0.5f * itemData.size.x, 0.5f * itemData.size.y);
        _itemCollider.size = itemData.size;
        _itemCollider.offset = _spriteRenderer.transform.localPosition;

        if (itemData.nonDestructible)
            nonDestructible = true;

        this.health = itemData.health;

    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (nonDestructible)
            return;
        if (health > 1)
            Instantiate(_hitFeedback, _spriteRenderer.transform.position, Quaternion.identity);
        else
            Instantiate(_destoyFeedback, _spriteRenderer.transform.position, Quaternion.identity);
        _spriteRenderer.transform.DOShakePosition(0.2f, 0.3f, 75, 1, false, true).OnComplete(ReduceHealth);
    }

    private void ReduceHealth()
    {
        health--;
        if (health <= 0)
        {
            _spriteRenderer.transform.DOComplete();
            Destroy(gameObject);
        }

    }
}