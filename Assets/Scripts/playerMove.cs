using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/RequireComponent.html
[RequireComponent(typeof(Rigidbody2D))]

public class playerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D; // maybe protected could be better??

    [SerializeField]
    public SO_movedata moveData;

    [SerializeField]
    protected float currentVelocity = 4f;
    protected Vector2 moveDirection;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void movePlayer(Vector2 playerInput)
    {
        if(playerInput.magnitude > 0)
        {
            // don't want instant movement, but also need to check vector direction
            // we need it between -1, 1 so should use dot product 

            if (Vector2.Dot(playerInput.normalized, moveDirection) < 0)
            {
                // decelerate appied when switching directions
                currentVelocity = 0;
                
            }
            moveDirection = playerInput.normalized;
        }
        
        currentVelocity = CalculateSpeed(playerInput);
        
    }

    private float CalculateSpeed(Vector2 playerInput)
    {
        if(playerInput.magnitude > 0)
        {
            currentVelocity += moveData.accelerate * Time.deltaTime;
        }
        else
        {
            currentVelocity -= moveData.decelerate * Time.deltaTime;
        }
        // Mathf.Clamp(value, min, max)
        return Mathf.Clamp(currentVelocity, 0, moveData.maximumVelocity);
    }

    private void FixedUpdate()
    {
        // based on earlier discussion with Dan
        _rigidbody2D.velocity = currentVelocity * moveDirection.normalized;
    }
}
