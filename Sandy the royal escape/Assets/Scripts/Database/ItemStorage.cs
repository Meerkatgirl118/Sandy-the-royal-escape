using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    // Collectables
    public int glassSlipperAmount = 0;

    // Weapons
    public bool fistUsable = true; // Weapon 0
    public bool knifeOwned = false; // Weapon 1
    public bool whipOwned = false; // Weapon 2
    public bool swordOwned = false; // Weapon 3
    public bool axeOwned = false; // Weapon 4

    public int fistStrengthBoost = 0;
    public int knifeStrengthBoost = 5;
    public int whipStrengthBoost = 10;
    public int swordStrengthBoost = 20;
    public int axeStrengthBoost = 30;

    public string[] weapons;
    public int[] weaponStrength;

    public List<string> weaponsAvailable = new List<string>();
    public int weaponInUse;

    void Start()
    {
        AssignWeaponStrength();

    }
    void AssignWeaponStrength()
    {
        weaponsAvailable.Add("fist");
        weaponStrength[0] = fistStrengthBoost;
        weaponStrength[1] = knifeStrengthBoost;
        weaponStrength[2] = whipStrengthBoost;
        weaponStrength[3] = swordStrengthBoost;
        weaponStrength[4] = axeStrengthBoost;
    }
}
