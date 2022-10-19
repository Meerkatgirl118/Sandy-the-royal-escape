using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    ItemStorage itemStorage;

    [SerializeField] int weaponSelected;
    void Start()
    {
        itemStorage = FindObjectOfType<ItemStorage>();
    }

    void Update()
    {
        WeaponInput();
    }

    void WeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && itemStorage.fistUsable)
        {
            weaponSelected = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && itemStorage.knifeOwned)
        {
            weaponSelected = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && itemStorage.whipOwned)
        {
            weaponSelected = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && itemStorage.swordOwned)
        {
            weaponSelected = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && itemStorage.axeOwned)
        {
            weaponSelected = 4;
        }

        itemStorage.weaponInUse = weaponSelected;
    }
}
