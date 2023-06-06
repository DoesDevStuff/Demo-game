using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Now if I want some other script to control enemy I can have them implement the input
/// from the CharInput and it will show the same in inspector too
/// </summary>
public class AILogic_Handler : MonoBehaviour, ICharInput
{
    [field: SerializeField]
    public GameObject Target { get; set; }

    [field: SerializeField]
    public AI_State currentState { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> onMoveKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent onShootKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent onShootKeyReleased { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> positionOfMouse { get; set; }

    private void Start()
    {
        //not sure if I want to acces the player directly or game object
        // so I guess I'll do gameobject like I did in flare as well
        Target = FindObjectOfType<Player>().gameObject;    
    }

    public void Update()
    {
        if(Target == null) // this is so that incase we don't call the movement to stop then enemy would continue to walk in direction it was assigned
        {
            onMoveKeyPressed?.Invoke(Vector2.zero); // will constantly remind enemy to stop
        }
        else
        {
            // else it will update the state and call any action on that state
            currentState.UpdatingState();
        }
    }

    public void Attacking()
    {
        onShootKeyPressed?.Invoke(); // So if any state has an action it wants to attack, we are going to call the attack from our AIEnemy
    }

    public void Move(Vector2 movementDirection, Vector2 targetPosition)
    {
        // Passing the movement direction and mouse position change since we want to make sure that our enemy rotates towards our player
        onMoveKeyPressed?.Invoke(movementDirection);
        positionOfMouse?.Invoke(targetPosition);
    }

    internal void ChangingToState(AI_State state)
    {
        currentState = state;
    }
}
