using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, ICharacter, IKnockBack
{
    // reference to enemy Data SO
    [field: SerializeField]
    public SO_EnemyData enemyData { get; set; }

    [field: SerializeField] 
    public int Health { get; set; } = 2;

    [field: SerializeField]
    public EnemyAttacking enemyAttack { get; set; }

    private bool _isEnemyDead = false;
    private CharMovement _charMovement;
    
    [field: SerializeField]
    public UnityEvent onGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent onDead { get; set; }

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
        Health = enemyData.MaxHealth;
    }

    public void GetHit(int damage, GameObject givesDamage)
    {
        if (_isEnemyDead == false)
        {
            Health--;
            onGetHit?.Invoke();

            if (Health <= 0)
            {
                _isEnemyDead = true;
                onDead?.Invoke();
                //Destroy(gameObject);
                //StartCoroutine(WaitTillDead());
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    /*
    IEnumerator WaitTillDead()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
    */

    // this will be attached to the onshoot event
    public void PerformAttack()
    {
        // check enemy not dead

        if(_isEnemyDead == false)
        {
            enemyAttack.EnemyAttack(enemyData.Damage);
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        _charMovement.KnockBack(direction, power, duration);
    }
}
