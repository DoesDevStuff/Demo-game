using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _playFeedbackInList = null;

    public void PlayFeedBack()
    {
        FinishFeedBack();

        foreach(var feedback in _playFeedbackInList)
        {
            feedback.CreateFeedback();
        }
    }

    private void FinishFeedBack()
    {
        foreach(var feedback in _playFeedbackInList)
        {
            feedback.CompletePreviousFeedback();
        }
    }
}
