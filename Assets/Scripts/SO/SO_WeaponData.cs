using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSO/weaponData")]
public class SO_WeaponData : ScriptableObject
{
    [field: SerializeField]
    public SO_BulletData BulletData { get; set; }

    [field: SerializeField]
    [field: Range(0,80)]
    public int ammoCapacity { get; set; }

    [field: SerializeField]
    public bool FiresAutomatically { get; set; } = false;

    [field: SerializeField]
    [field: Range(0.1f, 2f)]
    public float WeaponDelay { get; set; } = 0.2f;

    [field: SerializeField]
    [field: Range(0, 12)]
    public float BulletsSpreadAngle { get; set; } = 4;

    [field: SerializeField]
    private bool _isMultipleBullets = false;

    [field: SerializeField]
    [field:Range(1, 8)]
    private int _numberOfBullets = 1;

    internal int GetNumberOfBulletsToSpawn()
    {
        if (_isMultipleBullets) // is true
        {
            return _numberOfBullets;
        }
        return 1;
    }
}
