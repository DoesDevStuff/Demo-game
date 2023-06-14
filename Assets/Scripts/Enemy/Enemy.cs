using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // reference to enemy Data SO
    [field: SerializeField]
    public SO_EnemyData enemyData { get; set; }

    [field: SerializeField]
    public EnemyAttacking enemyAttack { get; set; }

    private bool _isEnemyDead = false;
    private CharMovement _charMovement;

    private void Awake()
    { // so now we can assign enemy attack by hand. Could also attach this to a weapon if i want that way it comes from a weapon and not the enemy
        if(enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttacking>();
        }
        _charMovement = GetComponent<CharMovement>();
    }

    private void Start()
    {
        GetComponent<CharacterHealth>().Health = enemyData.MaxHealth;
    }

    // this will be attached to the onshoot event
    public void PerformAttack()
    {
        // check enemy not dead

        if(_isEnemyDead == false)
        {
            enemyAttack.EnemyAttack(enemyData.Damage);
        }
    }
}
