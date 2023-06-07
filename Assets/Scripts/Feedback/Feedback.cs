using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we'd want to attach the classes here to our game objects
public abstract class Feedback : MonoBehaviour
{
    /// <summary>
    /// These are abstract classes in the event we have the feedback method called with another script action is yet to be finished
    /// Case in point like coroutines especially one that changes value over time, in case we call the feedback again while the coroutine is still executing
    /// so the previous feedback will reset the object state before restarting the feedback.
    /// </summary>

    public abstract void CreateFeedback(); // does whatever is needed to create feedback, eg: accesses Sprite render if that's needed
    public abstract void CompletePreviousFeedback();

    // prevent any situation where we are destroying the game object but coroutine is still running
    protected virtual void OnDestroy()
    {
        CompletePreviousFeedback();
    }
}
