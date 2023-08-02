using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    public static TimeController timeControllerInstance;

    private void Awake()
    {
        if(timeControllerInstance == null)
        {
            timeControllerInstance = this;
        }
        else if(timeControllerInstance != null)
        {
            Destroy(timeControllerInstance);
        }
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    public void ModifyTimeScale(float endTimeVal, float timeToWait, Action OnCompleteCallback = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeVal, timeToWait, OnCompleteCallback));
    }

    IEnumerator TimeScaleCoroutine(float endTimeVal, float timeToWait, Action OnCompleteCallback)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeVal;
        OnCompleteCallback?.Invoke();
    }
}
