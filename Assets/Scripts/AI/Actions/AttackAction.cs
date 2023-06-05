using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AITakeAction
{
    public override void TakeActions()
    {
        aiMovement_data.moveDirection = Vector2.zero; // has arrived stop chasing and get ready to attack
        aiMovement_data.areaOfInterest = aiEnemy_brain.Target.transform.position;

        aiEnemy_brain.Move(aiMovement_data.moveDirection, aiMovement_data.areaOfInterest);
        aiAction_data.isAttack = true;
        aiEnemy_brain.Attacking();
    }
}
