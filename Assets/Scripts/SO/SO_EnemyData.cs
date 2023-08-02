using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyData")]
public class SO_EnemyData : ScriptableObject
{   // fields are being used so that incase I want to set the values to private 
    // like {get; private set;} to limit access that someone else could have

    [field: SerializeField]
    public int MaxHealth { get; set; } = 4;

    [field: SerializeField]
    public int Damage { get; set; } = 1;
}
