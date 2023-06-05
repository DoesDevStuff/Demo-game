using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AITakeAction
{
    public override void TakeActions()
    {
        aiMovement_data.moveDirection = Vector2.zero;
        aiMovement_data.areaOfInterest = transform.position;

        aiEnemy_brain.Move(aiMovement_data.moveDirection, aiMovement_data.areaOfInterest);
    }
}
