using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script basically is what asks if -
/// 1. is player in range?
/// 2. is player visible to it?
/// And returns a true or false value
/// </summary>

public abstract class AIMakeDecisions : MonoBehaviour
{
    protected AIActionsData aiAction_data;
    protected AIMovementData aiMovement_data;
    protected AILogic_Handler aiEnemy_brain;

    private void Awake()
    {
        aiAction_data = transform.root.GetComponentInChildren<AIActionsData>();
        aiMovement_data = transform.root.GetComponentInChildren<AIMovementData>();
        aiEnemy_brain = transform.root.GetComponent<AILogic_Handler>();
    }

    public abstract bool MakeDecisions();
}
