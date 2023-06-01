using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWeaponSwitch : MonoBehaviour
{
    int totalWeapons = 1;

    [SerializeField]
    protected int currentWeaponIndex;
    
    [SerializeField]
    protected GameObject[] weapons;

    [SerializeField]
    protected GameObject MultiWeaponSwitchParent;

    [SerializeField]
    protected GameObject currentWeapon;

    public void Start()
    {
        totalWeapons = MultiWeaponSwitchParent.transform.childCount;
        weapons = new GameObject[totalWeapons];

        for(int i = 0; i < totalWeapons; i++)
        {
            weapons[i] = MultiWeaponSwitchParent.transform.GetChild(i).gameObject;
            gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }

    public void Update()
    {
        // next weapon
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentWeaponIndex < totalWeapons - 1)
            {
                weapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                weapons[currentWeaponIndex].SetActive(true);
                currentWeapon = weapons[currentWeaponIndex];
            }
        }

        // previous weapon
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentWeaponIndex > 0)
            {
                weapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                weapons[currentWeaponIndex].SetActive(true);
                currentWeapon = weapons[currentWeaponIndex];
            }
        }
    }
}
