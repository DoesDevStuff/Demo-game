using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AITakeAction
{
    public override void TakeActions()
    {
        var _direction = aiEnemy_brain.Target.transform.position - transform.position;
        aiMovement_data.moveDirection = _direction.normalized;
        aiMovement_data.areaOfInterest = aiEnemy_brain.Target.transform.position;

        // could call this AI logic Handler but it makes more sense to me to have this here
        aiEnemy_brain.Move(aiMovement_data.moveDirection, aiMovement_data.areaOfInterest);
    }
}
