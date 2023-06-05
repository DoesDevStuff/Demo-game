using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles if we should take any action and what
public class AIActionsData : MonoBehaviour
{
    [field: SerializeField]
    public bool isAttack { get; set; }

    [field: SerializeField]
    public bool isSpottedTarget { get; set; }

    [field: SerializeField]
    public bool isArrived { get; set; }
}
