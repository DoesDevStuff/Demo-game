using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores references of the abstract class
/// <summary>
/// List<AIMakeDecisions> contains all the decisions that we need to consider
/// before deciding whether to stay at current state or move to the next state
/// eg: decisions from idle to chase; and it will as if we are in range or visible
/// </summary>

public class AITransitionState : MonoBehaviour
{
    [field: SerializeField]
    public List<AIMakeDecisions> decisionsConsidered { get; set; }

    [field: SerializeField]
    public AI_State positiveOutput { get; set; } // if all decisions returns positive

    [field: SerializeField]
    public AI_State negativeOutput { get; set; } // what state we go to if even one of the decisions returns false

    private void Awake()
    {
        decisionsConsidered.Clear(); // List is cleared
        decisionsConsidered = new List<AIMakeDecisions>( GetComponents<AIMakeDecisions>() );
    }
}
