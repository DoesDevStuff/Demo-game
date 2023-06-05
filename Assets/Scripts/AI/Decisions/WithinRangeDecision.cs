using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinRangeDecision : AIMakeDecisions
{
    [field: SerializeField]
    [field: Range(0.1f, 12f)]
    public float Distance { get; set; } = 4.5f;

    public override bool MakeDecisions()
    {
        // checks if target (player) if it's position is in range and this script is on the main enemy parent
        if(Vector3.Distance(aiEnemy_brain.Target.transform.position, transform.position) < Distance)
        {
            // so if we haven't spotted target yet
            if(aiAction_data.isSpottedTarget == false)
            {
                aiAction_data.isSpottedTarget = true; // decision taken
            }
        }
        else
        {
            // if we don't see target
            aiAction_data.isSpottedTarget = false;
        }
        return aiAction_data.isSpottedTarget;
    }

    // visualise the distance
    protected void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Distance);
            Gizmos.color = Color.white;
        }
    }
}
