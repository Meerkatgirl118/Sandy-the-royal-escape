using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    // Collectables
    public int glassSlipperAmount = 0;

    // Weapons
    public bool knifeOwned; // Weapon 0
    public bool whipOwned; // Weapon 1
    public bool swordOwned; // Weapon 2
    public bool axeOwned; // Weapon 3

    public string[] weapons;
    public int weaponInUse;
}
