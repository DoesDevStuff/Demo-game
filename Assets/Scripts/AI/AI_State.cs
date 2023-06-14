using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// describes the general state and what we do when reaching the state is from AITakeAction.cs
public class AI_State : MonoBehaviour
{
    private AILogic_Handler _aiLogicHandling = null;

    [SerializeField]
    public List<AITakeAction> _takeActions = new List<AITakeAction>();
    [SerializeField]
    public List<AITransitionState> _transitions = new List<AITransitionState>();

    public AITransitionState lastTransition { get; set; }     

    private void Awake()
    {
        _aiLogicHandling = transform.root.GetComponent<AILogic_Handler>();
    }

    public void UpdatingState()
    {
        foreach(var action in _takeActions)
        {
            action.TakeActions();
        }

        foreach(var transition in _transitions)
        {
            // checks while player in range --> True --> return to idle
            // while the player is visible to enemy --> True --> Chase

            bool decisionResult = false;

            foreach(var decisionMaking in transition.decisionsConsidered)
            {
                decisionResult = decisionMaking.MakeDecisions();
                if (decisionResult == false) break;  // one of the decisions doesn't meet criteria
            }

            if (decisionResult)
            { // if all criteria are met i.e results for decisions are true
                if (transition.positiveOutput != null)
                {
                    lastTransition = transition;
                    _aiLogicHandling.ChangingToState(transition.positiveOutput);
                    return;
                }
            }
            // if we don't assign this negative output then it'll break and go back to our updating State
            // and go look for the next transition in list and its decision.
            else
            {
                if (transition.negativeOutput != null)
                {
                    lastTransition = transition;
                    _aiLogicHandling.ChangingToState(transition.negativeOutput);
                    return;
                }
            }
        }
    }
}
