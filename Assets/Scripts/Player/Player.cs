using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, ICharacter
{
    public int Health { get; set; }

    public UnityEvent onDead { get; set; }
    
    public UnityEvent onGetHit { get; set; }

    
}
