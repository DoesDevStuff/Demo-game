using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// https://docs.unity3d.com/Manual/UnityEvents.html


public class playerInput : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<Vector2> onMoveKeyPressed;

    private void Update()
    {
        GetMoveInput();
    }

    private void GetMoveInput()
    { 
        // movement check, receives axis values from unity's input manager
        onMoveKeyPressed?.Invoke(new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ) ;
    }
}
