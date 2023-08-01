using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class BulletShellGenerator : ObjectPooling
{
    [SerializeField]
    private float _flyDuration = 0.2f;
    [SerializeField]
    private float _flyStrength = 1.2f;

    // to spawn the bullets I can use unity events again 
    // because I already have the On Shoot Event done before
    public void SpawnBulletShell()
    {
        var bulletShell = SpawnObject();
        MoveShellInRandomDirection(bulletShell);
    }

    private void MoveShellInRandomDirection(GameObject bulletShell)
    {
        bulletShell.transform.DOComplete();
        var randomDirection = Random.insideUnitCircle;

        /// <summary> for the random direction:
        /// if the bullet was to fly upwards, we are going to turn it to fly downwards 
        /// and it will make the bullets fly either left, right 
        /// or anywhere in between in the downwards direction.
        /// </summary>
        randomDirection = randomDirection.y > 0 ? new Vector2(randomDirection.x, -randomDirection.y) : randomDirection;

        bulletShell.transform.DOMove(((Vector2)transform.position + randomDirection) * _flyStrength,
            _flyDuration).OnComplete(() => bulletShell.GetComponent<AudioSource>().Play());

        //euler angles
        bulletShell.transform.DORotate(new Vector3(0, 0, Random.Range(0f, 360f)), _flyDuration);
    }
}
