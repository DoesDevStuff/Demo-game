using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack_Enemy : EnemyAttacking
{
    public override void EnemyAttack(int damage)
    {
        if(isWaitTillNextAttack == false)
        {
            var hittable = GetTarget().GetComponent<IHittable>();
            hittable?.GetHit(damage, gameObject);
            StartCoroutine(WaitTillNextAttackCoroutine());
        }
        
    }
}
