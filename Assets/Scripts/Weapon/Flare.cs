using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : Bullet
{
    // the way bouncy flares work is with a circle cast that uses the reflection equation to bounce them on detection
    // while in this state it also rotates to match its velocity
    // after the flare has bounced more than the max bounces the raycast is disabled and a collider is attached to the flare

    // sticky flares just parent themselves on hit
    // they dont work properly if the object is scaled

    public bool stickyFlare = false;
    [HideInInspector] public FlareGun flareGunReference;

    [SerializeField]
    protected float lifetimeOnSettle = 120f;

    [SerializeField]
    protected int maxBounces = 2;

    [SerializeField]
    protected float baseBounciness = 0.8f;

    [SerializeField]
    protected float bounceChangeFactor = 0.4f;

    [SerializeField]
    protected Vector2 boxColliderSize;

    [SerializeField]
    protected float bounceColliderRadius = 0.1f;

    [SerializeField]
    protected LayerMask bounceMask;

    protected Rigidbody2D rb2D;

    private int _bounceCounter = 0;
    private float _bounciness;
    private bool _bounceEntrance = false;
    private float _lifetimer;
    private bool _stuck;

    public override SO_BulletData BulletData
    {
        get => base.BulletData;

        set
        {
            base.BulletData = value;
            rb2D = GetComponent<Rigidbody2D>();
            rb2D.drag = BulletData.Friction;
        }
    }

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        _bounciness = baseBounciness;

        _lifetimer = lifetimeOnSettle;
    }

    // Update is called once per frame
    void Update()
    {
        if (stickyFlare == false)
        {
            if (_bounceCounter < maxBounces)
            {
                RotateToTrajectory();
            }
            else
            {
                _lifetimer = _lifetimer - Time.deltaTime;

                if (_lifetimer <= 0)
                {
                    flareGunReference.spawnedFlares.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (_stuck == true)
            {
                _lifetimer = _lifetimer - Time.deltaTime;

                if (_lifetimer <= 0)
                {
                    flareGunReference.spawnedFlares.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            else
            {
                RotateToTrajectory();
                Stick();
            }
        }
    }

    void FixedUpdate()
    {
        if (_bounceCounter < maxBounces && stickyFlare == false)
        {
            Bounce();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bounceColliderRadius);
        Gizmos.DrawWireCube(transform.position, boxColliderSize * transform.localScale);
    }

    void Bounce()
    {
        RaycastHit2D bounceCollider = Physics2D.CircleCast(transform.position, bounceColliderRadius, new Vector2(rb2D.velocity.x, rb2D.velocity.y), 0, bounceMask);

        if (bounceCollider.collider != null && _bounceEntrance == false)
        {
            _bounceEntrance = true;

            Vector2 d = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
            Vector2 n = bounceCollider.normal;

            Vector2 r = d - 2 * Vector2.Dot(d, n) * n;

            rb2D.velocity = new Vector2(0, 0);
            rb2D.AddForce(r * _bounciness, ForceMode2D.Impulse);

            _bounceCounter++;
            _bounciness = _bounciness * bounceChangeFactor;

            if (_bounceCounter >= maxBounces)
            {
                BoxCollider2D boxColl2D = gameObject.AddComponent<BoxCollider2D>();
                boxColl2D.size = boxColliderSize;
            }
        }
        else
        {
            _bounceEntrance = false;
        }
    }

    void Stick()
    {
        RaycastHit2D stickCollider = Physics2D.CircleCast(transform.position, bounceColliderRadius, new Vector2(rb2D.velocity.x, rb2D.velocity.y), 0, bounceMask);

        if (stickCollider.collider != null)
        {
            gameObject.transform.SetParent(stickCollider.collider.gameObject.transform);

            rb2D.velocity = new Vector2(0, 0);
            rb2D.bodyType = RigidbodyType2D.Static;

            _stuck = true;
        }
    }

    void RotateToTrajectory()
    {
        float angle = Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
