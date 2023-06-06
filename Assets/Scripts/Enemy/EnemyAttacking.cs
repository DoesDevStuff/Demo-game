using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttacking : MonoBehaviour
{
    protected AILogic_Handler _enemyBrain; 

    [field: SerializeField]
    public float AttackDelay { get; private set; } = 1; // don't want to set it anywhere but here
    protected bool isWaitTillNextAttack;

    private void Awake()
    {
        _enemyBrain = GetComponent<AILogic_Handler>();
    }

    public abstract void EnemyAttack(int damage); // will implemnt some sort of melee attack or shooting bullet

    protected IEnumerator WaitTillNextAttackCoroutine()
    {
        isWaitTillNextAttack = true;
        yield return new WaitForSeconds(AttackDelay);
        isWaitTillNextAttack = false;
    }

    protected GameObject GetTarget()
    {
        return _enemyBrain.Target;
    }
}
