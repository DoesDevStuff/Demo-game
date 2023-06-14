using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeFeedback : Feedback
{
    [SerializeField]
    private float _freezeTimeDelay = 0.01f, _unfreezeTimeDelay = 0.02f;

    [SerializeField]
    [Range(0, 1)]
    private float _timeFreezeValue = 0.2f;

    public override void CompletePreviousFeedback()
    {
        if(TimeController.timeControllerInstance != null)
        {
            TimeController.timeControllerInstance.ResetTimeScale();
        }
    }

    /// <summary>
    /// What we're doing here is modifying the timescale 
    /// 1. _timeFreezeValue of 0.2f and it will do so after some time freeze delay which could also be zero
    /// 2. After the _freezeTimeDelay we wait for the _unfreezeTimeDelay in this case it's 0.02 seconds
    /// 3. Then we restart this and reset the timescale value to 1  and return the time to normal scale
    /// </summary>

    public override void CreateFeedback()
    {
        TimeController.timeControllerInstance.ModifyTimeScale(_timeFreezeValue, _freezeTimeDelay,
            () => TimeController.timeControllerInstance.ModifyTimeScale(1, _unfreezeTimeDelay));
    }
}
