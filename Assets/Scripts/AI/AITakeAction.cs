using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// will tell us what actions to take when we reach a specific enemy ai state. (remember the FSM diagram)
public abstract class AITakeAction : MonoBehaviour
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

    public abstract void TakeActions();
}
