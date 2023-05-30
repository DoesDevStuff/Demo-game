using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSO/BulletData")]
public class SO_BulletData : ScriptableObject
{
    [field: SerializeField]
    public GameObject bulletPrefab { get; set; }

    [field: SerializeField]
    [field: Range(1, 60)]
    public float BulletSpeed { get; set; }

    [field: SerializeField]
    public int Damage { get; set; } = 1;

    [field: SerializeField]
    [field: Range(0, 20)]
    public float Friction { get; set; }

    [field: SerializeField]
    public bool isBouncy { get; set; } = false;

    [field: SerializeField]
    public bool isGoThroughHittable { get; set; } = false;

    [field: SerializeField]
    public bool isRayCast { get; set; } = false;

    [field: SerializeField]
    public GameObject ImpactObstaclePrefab { get; set; }
    [field: SerializeField]
    public GameObject ImpactEnemyPrefab { get; set; }

    [field: SerializeField]
    [field: Range(1f, 15f)]
    public float knockBackPower { get; set; } = 2;

    [field: SerializeField]
    [field: Range(0.01f, 0.5f)]
    public float KnockBackDelay { get; set; } = 0.1f;
}
