using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// reference for what the enemy does : https://enterthegungeon.fandom.com/wiki/Cult_of_the_Gundead

//handles if we should move and where to
public class AIMovementData : MonoBehaviour
{
    [field: SerializeField]
    public Vector2 moveDirection { get; set; }

    [field: SerializeField]
    public Vector2 areaOfInterest { get; set; } // position where player was last seen.

}
