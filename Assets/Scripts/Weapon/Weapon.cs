using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// shoots bullets
public class Weapon : MonoBehaviour
{
    public void Shoot()
    {
        Debug.Log("Shots Fired");
    }

    public void StopShooting()
    {
        Debug.Log("No Bullets shot");
    }
}
