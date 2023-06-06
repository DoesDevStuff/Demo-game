using UnityEngine;
using UnityEngine.Events;

public interface ICharacter
{
    int Health { get; set; }
    UnityEvent onDead { get; set; }
    UnityEvent onGetHit { get; set; }
    
}