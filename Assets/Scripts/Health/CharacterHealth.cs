using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHittable
{
    [field: SerializeField]
    public int Health { get; set; }

    bool _isDead = false;

    List<IDamageHandler> _damageHandlers = new List<IDamageHandler>();
    List<IDeathHandler> _deathHandlers = new List<IDeathHandler>();


    void Awake()
    {
        GetComponentsInChildren(_damageHandlers);
        GetComponentsInChildren(_deathHandlers);
    }

    public void GetHit(int damage, GameObject givesDamage)
    {
        if(_isDead)
            return;

        Health -= damage;

        foreach(var handler in _damageHandlers)
            handler.OnDamage(damage, givesDamage);

        if(Health <= 0)
        {
            _isDead = true;

            foreach(var handler in _deathHandlers)
                handler.OnDeath();
        }
    }
}
