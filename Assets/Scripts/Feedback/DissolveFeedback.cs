using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DissolveFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _duration = 0.05f;

    [field: SerializeField]
    public UnityEvent onDeathCallback { get; set; }

    public override void CompletePreviousFeedback()
    {
        _spriteRenderer.DOComplete();
        _spriteRenderer.material.DOComplete();
    }

    public override void CreateFeedback()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_spriteRenderer.material.DOFloat(0, "_Dissolve", _duration));

        if(onDeathCallback != null)
        {
            sequence.AppendCallback(() => onDeathCallback.Invoke());
        }
    }
}
