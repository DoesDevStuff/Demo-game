using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DestroyedEvent))]

public class Player : MonoBehaviour, ICharacter, IHittable
{
    [HideInInspector] public DestroyedEvent destroyedEvent;
    private bool _isDead = false;

    [SerializeField]
    private int _maxHealth = 2;
    private int _health;
    
    public int Health {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            uiHealth.UpdateUI(_health);
        }
    }

    [field: SerializeField]
    public UIHealth uiHealth { get; set; }

    [field: SerializeField]
    public UnityEvent onDead { get; set; }

    [field: SerializeField]
    public UnityEvent onGetHit { get; set; }

    private void Awake()
    {
        destroyedEvent = GetComponent<DestroyedEvent>();
    }
    private void Start()
    {
        Health = _maxHealth;
        uiHealth.InitialiseLives(Health);
    }

    public void GetHit(int damage, GameObject givesDamage)
    {
        if(_isDead == false)
        {
            Health -= damage;
            onGetHit?.Invoke();
            if(Health <= 0)
            {

                onDead?.Invoke(); // going to rely on onDead to take care of the player death anim
                _isDead = true;
                //StartCoroutine(DeathCoroutine());
                destroyedEvent.CallDestroyedEvent(true, 0);
            }
        }
        //Debug.Log("Player is Hit");
    }

    /*
    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    */
}
