using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    // reference to enemy Data SO
    [field: SerializeField]
    public SO_EnemyData enemyData { get; set; }

    [field: SerializeField] 
    public int Health { get; set; } = 2;

    [field: SerializeField]
    public UnityEvent onGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent onDead { get; set; }

    private void Start()
    {
        Health = enemyData.MaxHealth;
    }

    public void GetHit(int damage, GameObject givesDamage)
    {
        Health--;
        onGetHit?.Invoke();

        if(Health <= 0)
        {
            onDead?.Invoke();
            //Destroy(gameObject);
            StartCoroutine(WaitTillDead());
        }
    }

    IEnumerator WaitTillDead()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}
