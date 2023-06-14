using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// https://docs.unity3d.com/ScriptReference/RequireComponent.html
[RequireComponent(typeof(Rigidbody2D))]

public class CharMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    public SO_movedata moveData;

    [SerializeField]
    protected float currentVelocity = 4f;
    protected Vector2 moveDirection;

    [SerializeField]
    public UnityEvent<float> onSpeedChange;
    
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
        ///checks to see if anything is listening
        /// if not null then we pass the current Velocity.
        /// we only care what happens to character here and not the actu

        onSpeedChange?.Invoke(currentVelocity);

        // based on earlier discussion with Dan
        _rigidbody2D.velocity = currentVelocity * moveDirection.normalized;
    }

    public void StopInstantly()
    {
        currentVelocity = 0;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
