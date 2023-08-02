using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_weapon : CharWeapon
{
    [SerializeField]
    private UIBullets _uiAmmo = null, _uiFlareAmmo = null;

    public bool AmmoFull { get => weapon != null && weapon.IsammoFull;}
    public bool FlareAmmoFull { get => flareGun != null && flareGun.IsammoFull; }

    private void Start()
    {
        if(weapon != null)
        {
            weapon.OnAmmoChange.AddListener(_uiAmmo.UpdateBulletsText);
            _uiAmmo.UpdateBulletsText(weapon.GunAmmo);
        }

        if(flareGun != null)
        {
            flareGun.OnFlareAmmoChange.AddListener(_uiFlareAmmo.UpdateFlareText);
            _uiFlareAmmo.UpdateFlareText(flareGun.GunAmmo);
        }
    }

    public void AddAmmo(int amount)
    {
        if(weapon != null)
        {
            weapon.GunAmmo += amount;
        }

        if(flareGun != null)
        {
            flareGun.GunAmmo += amount;
        }
    }

}
