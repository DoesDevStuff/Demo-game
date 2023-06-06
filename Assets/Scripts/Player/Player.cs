using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, ICharacter, IHittable
{
    [field: SerializeField]
    public int Health { get; set; }

    private bool _isDead = false;

    [field: SerializeField]
    public UnityEvent onDead { get; set; }

    [field: SerializeField]
    public UnityEvent onGetHit { get; set; }

    public void GetHit(int damage, GameObject givesDamage)
    {
        if(_isDead == false)
        {
            Health--;
            onGetHit?.Invoke();
            if(Health <= 0)
            {
                onDead?.Invoke();
                _isDead = true;
            }
        }
        //Debug.Log("Player is Hit");
    }
}
