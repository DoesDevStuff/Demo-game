using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    protected Rigidbody2D rgdbdy2d;
    private bool isDead = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // protects us from the instance of hitting two enemies at once
        if (isDead) return;
        isDead = true;

        var enemy_hittable = collision.GetComponent<IHittable>();
        enemy_hittable?.GetHit(BulletData.Damage, gameObject); // since the bullet is storing how much damage we are dealing

        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            HitObstacle(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }
        Destroy(gameObject);
    }

    private void HitEnemy(Collider2D collision)
    {
        Vector2 randomOffset = Random.insideUnitCircle * 0.25f;
        Instantiate(BulletData.ImpactEnemyPrefab, collision.transform.position + (Vector3)randomOffset, Quaternion.identity);
    }

    private void HitObstacle(Collider2D collision)
    {
        // shoot raycast in direction of bullet and find closest point where bullet hits obstacle
        // this is because our floor is one tilemap
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, BulletData.BulletlayerMask);
        if(hit.collider != null)
        {
            Instantiate(BulletData.ImpactObstaclePrefab, hit.point, Quaternion.identity);
        }
    }
}
