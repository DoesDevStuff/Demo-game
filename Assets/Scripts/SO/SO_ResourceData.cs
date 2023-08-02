using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ResourceData")]
public class SO_ResourceData : ScriptableObject
{
    [SerializeField]
    private int _minAmount = 1, _maxAmount = 5;

    [field: SerializeField]
    public ResourceTypeEnum ResourceType { get; set; }

    public int GetAmount()
    {
        // range in min exclusive and max exclusive hence the +1 so that max value can be achieved
        return Random.Range(_minAmount, _maxAmount + 1);
    }
}

public enum ResourceTypeEnum
{
    None,
    Health,
    Ammo
}