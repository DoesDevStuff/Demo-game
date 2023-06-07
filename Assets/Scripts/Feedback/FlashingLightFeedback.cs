using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashingLightFeedback : Feedback
{
    [SerializeField]
    private Light2D _lightTarget = null;

    [SerializeField]
    private float _lightFlashOnDelay = 0.01f, _lightFlashOffDelay = 0.01f;

    [SerializeField]
    private bool _isDefaultState = false;

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        _lightTarget.enabled = _isDefaultState;
    }

    public override void CreateFeedback()
    {
        StartCoroutine(ToggleLightCoroutine(_lightFlashOnDelay, true, () => 
            StartCoroutine(ToggleLightCoroutine(_lightFlashOffDelay, false) ) ) );
    }

    // So we are going to wait a bit and then we are going to invoke the finished callback if we have it. It uses System Actions similar to events
    // this way we are going to be able to toggle the light on, then wait a bit
    // And then we are going to add another coroutine as the callback.
    IEnumerator ToggleLightCoroutine(float _time, bool _result, Action FinishCallback = null)
    {
        yield return new WaitForSeconds(_time);
        _lightTarget.enabled = _result;
        FinishCallback?.Invoke();
    }
    
}
