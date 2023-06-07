using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using DoTween : http://dotween.demigiant.com/documentation.php
// resource looked at as well : https://www.youtube.com/watch?v=NKl27_jEpzA

public class ShakeFeedback : Feedback
{
    [SerializeField]
    private GameObject _objectToShake = null;

    [SerializeField]
    private float _duration = 0.2f, _strength = 1, _randomness = 45;

    [SerializeField]
    private int _vibrato = 8;

    [SerializeField]
    private bool _isSnapping = false, _isFadeout = true; 


    public override void CompletePreviousFeedback()
    {
        _objectToShake.transform.DOComplete();
    }

    public override void CreateFeedback()
    {
        CompletePreviousFeedback();
        _objectToShake.transform.DOShakePosition(_duration, _strength, _vibrato, _randomness, _isSnapping, _isFadeout);
    }
}
