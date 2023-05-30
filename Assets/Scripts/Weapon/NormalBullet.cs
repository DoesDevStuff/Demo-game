using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    protected Rigidbody2D rgdbdy2d;

    // override Bullet data properties

    public override SO_BulletData BulletData
    {
        get => base.BulletData;

        set
        {
            base.BulletData = value;
            rgdbdy2d = GetComponent<Rigidbody2D>();
            rgdbdy2d.drag = BulletData.Friction;
        }
    }

    private void FixedUpdate()
    {
        // since we're playing with rigidbody use fixed update

        if(rgdbdy2d != null && BulletData != null)
        {
            // since it's kinematic we'd need to look at its position and not speed
            // flies in direction we're shooting
            rgdbdy2d.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }
}