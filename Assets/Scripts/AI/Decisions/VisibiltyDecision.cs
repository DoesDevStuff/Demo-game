using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisibiltyDecision : AIMakeDecisions
{
    [field: SerializeField]
    [field: Range(0.1f, 12f)]
    public float Distance { get; set; } = 4.5f;

    [SerializeField]
    private LayerMask _raycastMask = new LayerMask();

    [field: SerializeField]
    public UnityEvent onPlayerSpotted { get; set; }

    public override bool MakeDecisions()
    {
        var direction = aiEnemy_brain.Target.transform.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Distance, _raycastMask);

        if( (hit.collider != null) && (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") ) )
        {
            onPlayerSpotted?.Invoke();
            return true;
        }
        return false;
    }

    // debug and visualise distance
    protected void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject && aiEnemy_brain != null && aiEnemy_brain.Target != null)
        {
            Gizmos.color = Color.blue;
            var direction = aiEnemy_brain.Target.transform.position - transform.position;
            Gizmos.DrawRay(transform.position, direction.normalized * Distance);
            Gizmos.color = Color.white;
        }
    }
}
