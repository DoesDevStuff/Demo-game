using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlareGun : MonoBehaviour
{
    // the flare gun needs a empty game object attached to player for it's script and a gameobject attached to that gameobject, representing the firing point
    // the firing point must be called "FlareSpawnPoint"

    [Header("Sticky")]
    public bool shootStickyFlares;

    [SerializeField]
    protected GameObject FlareSpawnPoint;

    [HideInInspector] public List<GameObject> spawnedFlares = new List<GameObject>();

    [SerializeField]
    protected int maxSpawnedFlares = 5;

    [SerializeField]
    protected float gunImpulsePower = 7f;

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
        FlareSpawnPoint = transform.Find("FlareSpawnPoint").gameObject;
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            shootStickyFlares = !shootStickyFlares;
        }

        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && reloadGunCoroutine == false)
        {
            if (GunAmmo > 0)
            {
                GunAmmo--;
                OnShooting?.Invoke();

                for (int i = 0; i < weaponData.GetNumberOfBulletsToSpawn(); i++)
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
        SpawnFlare(FlareSpawnPoint.transform.position, CalculateShootAngle(FlareSpawnPoint));
    }

    private void SpawnFlare(Vector3 position, Quaternion rotation)
    {
        var instantiatedFlare = Instantiate(weaponData.BulletData.flarePrefab, position, rotation);
        instantiatedFlare.GetComponent<Bullet>().BulletData = weaponData.BulletData;

        Rigidbody2D flareRB2D = instantiatedFlare.GetComponent<Rigidbody2D>();
        Flare flareScript = instantiatedFlare.GetComponent<Flare>();
        flareScript.flareGunReference = this;

        if (shootStickyFlares == true)
        {
            flareScript.stickyFlare = true;
        }

        spawnedFlares.Add(instantiatedFlare);
        if (spawnedFlares.Count > maxSpawnedFlares)
        {
            Destroy(spawnedFlares[0]);
            spawnedFlares.RemoveAt(0);
        }
        float rotationInRadians = gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float vectorX = Mathf.Cos(rotationInRadians);
        float vectorY = Mathf.Sin(rotationInRadians);

        Debug.Log(rotationInRadians);

        flareRB2D.AddForce(new Vector2(vectorX, vectorY) * gunImpulsePower, ForceMode2D.Impulse);

    }

    private Quaternion CalculateShootAngle(GameObject FlareSpawnPoint)
    {
        float spreadAngle = Random.Range(-weaponData.BulletsSpreadAngle, weaponData.BulletsSpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spreadAngle));

        return FlareSpawnPoint.transform.rotation * bulletSpreadRotation; // adding two quaternions here
    }
}
