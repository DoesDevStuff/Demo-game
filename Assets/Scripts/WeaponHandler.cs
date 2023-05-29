using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{   
    
    protected float weaponRotateAngle;
    [SerializeField]
    protected WeaponRenderer weaponRenderer;

    private void Awake()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
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
        // check weapon rendered not null then check for the angle 
        weaponRenderer?.FlipWeaponSprite(weaponRotateAngle > 90 || weaponRotateAngle < -90);

        // similarly check it's not null and then call RenderBehindPlayer
        weaponRenderer?.RenderBehindPlayer(weaponRotateAngle < 180 && weaponRotateAngle > 0);
    }
}
