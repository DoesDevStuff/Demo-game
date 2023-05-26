using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  = "PlayerSO/movementData")]

public class SO_movedata : ScriptableObject
{
    [Range(1, 10)]
    public float maximumVelocity = 4;

    [Range(0.2f, 100)]
    public float accelerate = 10, decelerate = 10;
}
    

