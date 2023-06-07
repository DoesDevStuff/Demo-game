using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineShakeFeedback : Feedback
{
    [SerializeField]
    private CinemachineVirtualCamera _cinemachineCam;

    [SerializeField]
    [Range(0, 4)]
    private float _amplitude = 1, _intensity = 1;

    [SerializeField]
    [Range(0, 1)]
    private float _duration = 0.1f;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void Start()
    {
        if(_cinemachineCam == null)
        {
            _cinemachineCam = FindObjectOfType<CinemachineVirtualCamera>();
            _noise = _cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        _noise.m_AmplitudeGain = 0;
    }

    public override void CreateFeedback()
    {
        _noise.m_AmplitudeGain = _amplitude;
        _noise.m_FrequencyGain = _intensity;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        for(float i = _duration; i > 0; i -= Time.deltaTime)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, (i / _duration));
            yield return null; // do every frame
        }
        _noise.m_AmplitudeGain = 0;
    }
}
