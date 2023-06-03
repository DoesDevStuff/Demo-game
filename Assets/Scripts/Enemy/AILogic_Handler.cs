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
}
