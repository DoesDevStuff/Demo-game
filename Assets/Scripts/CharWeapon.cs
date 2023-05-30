using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWeapon : MonoBehaviour
{   
    protected float weaponRotateAngle;

    [SerializeField]
    protected WeaponRenderer weaponRenderer;

    [SerializeField]
    protected Weapon weapon;

    private void Awake()
    {
        AssignPlayerWeapon();
    }

    private void AssignPlayerWeapon()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimingWeapon(Vector2 mousePosition)
    {   //this is to make sure our weapon follows the mouse as well. And ofcourse rotates.
        var aimingDir = (Vector3)mousePosition - transform.position;
        weaponRotateAngle = Mathf.Atan2(aimingDir.y, aimingDir.x) * Mathf.Rad2Deg; // inverse tan to get theta basically
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(weaponRotateAngle, Vector3.forward); // 2d view so we rotate in only z. 
    }

    protected void AdjustWeaponRendering()
    {
        if(weaponRenderer != null)
        {
            // check weapon rendered not null then check for the angle 
            weaponRenderer.FlipWeaponSprite(weaponRotateAngle > 90 || weaponRotateAngle < -90);

            // similarly check it's not null and then call RenderBehindPlayer
            weaponRenderer.RenderBehindPlayer(weaponRotateAngle < 180 && weaponRotateAngle > 0);
        }
        
    }


    public void Shooting()
    {
        if(weapon != null)
        {
            weapon.TryShooting(); // checks that we have object of shooting in weapon. It checks if not null here
        }
        
    }

    public void StopShooting()
    {
        if (weapon != null)
        {
            weapon.StopShooting();
        }
    }   
}
