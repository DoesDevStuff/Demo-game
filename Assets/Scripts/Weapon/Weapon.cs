using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// shoots bullets
public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject shootPoint;

    [SerializeField]
    protected int _gunAmmo = 10;

    [SerializeField]
    protected SO_WeaponData weaponData;

    public int GunAmmo
    {
        get
        {
            return _gunAmmo;
        }

        set
        {
            _gunAmmo = Mathf.Clamp(value, 0, weaponData.ammoCapacity);
        }
    }

    // useful if I want to pick up more ammo 
    public bool IsammoFull { get => GunAmmo >= weaponData.ammoCapacity; }

    protected bool isShooting = false;

    [field: SerializeField]
    protected bool reloadGunCoroutine = false;

    [field: SerializeField]
    public UnityEvent OnShooting { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootButNoAmmo { get; set; }


    public void Start()
    {
        GunAmmo = weaponData.ammoCapacity; // start with full bullets
    }

    public void TryShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public void ReloadGun(int _gunAmmo)
    {
        GunAmmo += _gunAmmo;
    }

    public void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if(isShooting && reloadGunCoroutine == false)
        {
            if(GunAmmo > 0)
            {
                GunAmmo--;
                OnShooting?.Invoke();

                for(int i = 0; i < weaponData.GetNumberOfBulletsToSpawn(); i++)
                {
                    ShootBullets();
                }
            }
            else
            {
                isShooting = false;
                OnShootButNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShotCoroutine());
        if (weaponData.FiresAutomatically == false)
        {
            isShooting = false; // player needs to re click to start shooting
        }
    }

    private IEnumerator DelayNextShotCoroutine()
    {
        reloadGunCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        reloadGunCoroutine = false;
    }

    private void ShootBullets()
    {
        SpawnBullet(shootPoint.transform.position, CalculateShootAngle(shootPoint));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.bulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateShootAngle(GameObject shootPoint)
    {
        float spreadAngle = Random.Range(-weaponData.BulletsSpreadAngle, weaponData.BulletsSpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spreadAngle));

        return shootPoint.transform.rotation * bulletSpreadRotation; // adding two quaternions here
    }
    
}
