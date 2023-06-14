using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// https://docs.unity3d.com/Manual/UnityEvents.html

/// <summary>
/// This script handles the player input.
/// I want to this in a similar fashion in the enemy but have it driven by the AI logic
/// Looked at how to extract the properties and it seems like creating an interface for this is probably the best idea.
/// That way player and enemy will have the same events.
/// </summary>

public class CharInput : MonoBehaviour, ICharInput, IDeathHandler
{
    private Camera _mainCamera;
    private bool _shootButtonDown = false;

    [field: SerializeField]
    public UnityEvent<Vector2> onMoveKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> positionOfMouse { get; set; }

    [field: SerializeField]
    public UnityEvent onShootKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent onShootKeyReleased { get; set; }

    private void Awake()
    {
        _mainCamera = Camera.main; // get reference to the camera in scene (make sure it's tagged)
    }

    private void Update()
    {
        GetMoveInput();
        GetPositionOfMouse();
        GetShootInput();
    }

    private void GetShootInput()
    {
       if (Input.GetAxisRaw("Fire1") > 0)
        {
            if(_shootButtonDown == false)
            {
                _shootButtonDown = true;
                onShootKeyPressed?.Invoke(); // added listener
            }
            
        }
        else
        {
            if (_shootButtonDown == true)
            {
                _shootButtonDown = false;
                onShootKeyReleased?.Invoke();
            }
            
        }
    }

    private void GetPositionOfMouse()
    { // returns the X and Y position of the mouse, additionally z for mouse can be set to main camera clipping plane to work no matter what depth we set
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _mainCamera.nearClipPlane;

        var mouseWorldSpace = _mainCamera.ScreenToWorldPoint(mousePos);
        positionOfMouse?.Invoke(mouseWorldSpace);
    }

    private void GetMoveInput()
    { 
        // movement check, receives axis values from unity's input manager
        onMoveKeyPressed?.Invoke(new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ) ;
    }

    public void OnDeath()
    {
        enabled = false;
    }
}
