using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EquipMenu : MonoBehaviour
{
    ItemStorage itemStorage;
    SwitchWeapon switchWeapon;
    [SerializeField] CinemachineFreeLook virtualCamera;

    [SerializeField] GameObject weapon0;
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;
    [SerializeField] GameObject weapon3;
    [SerializeField] GameObject weapon4;

    [SerializeField] int weaponChoiceSelected = 0;
    [SerializeField] int totalWeapons;
    public List<bool> weaponsSelectable = new List<bool>();

    public int directionPressed = 0; // 0 = previous, 1 = next

    void Start()
    {
        itemStorage = FindObjectOfType<ItemStorage>();
        switchWeapon = FindObjectOfType<SwitchWeapon>();
        virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        SetWeaponList();
        virtualCamera.enabled = false;
    }

    void Update()
    {
        DisplayWeapon();
    }

    void SetWeaponList()
    {
        totalWeapons = itemStorage.weaponsAvailable.Count;
    }

    public void NextButton()
    {
        directionPressed = 1;
        if (weaponChoiceSelected < totalWeapons)
        {
            weaponChoiceSelected++;
        }
        else
        {
            weaponChoiceSelected = 0;
        }
    }

    public void PreviousButton()
    {
        directionPressed = 0;
        if (weaponChoiceSelected > 0)
        {
            weaponChoiceSelected--;
        }
        else
        {
            weaponChoiceSelected = totalWeapons;
        }
    }

    void DisplayWeapon()
    {
        SetWeaponList();
        switch (weaponChoiceSelected)
        {
            case 0:
                DisableAllWeaponImages();
                weapon0.SetActive(true);
                break;
            case 1:
                DisableAllWeaponImages();
                CheckWeaponAvailablity(itemStorage.knifeOwned, weapon1);
                break;
            case 2:
                DisableAllWeaponImages();
                CheckWeaponAvailablity(itemStorage.whipOwned, weapon2);
                break;
            case 3:
                DisableAllWeaponImages();
                CheckWeaponAvailablity(itemStorage.swordOwned, weapon3);
                break;
            case 4:
                DisableAllWeaponImages();
                if (!itemStorage.axeOwned) { weaponChoiceSelected = 0; }
                else { weapon4.SetActive(true); }
                break;
        }
    }

    public void CheckWeaponAvailablity(bool weaponOwned, GameObject weaponImage)
    {
        if (!weaponOwned)
        {
            switch (directionPressed)
            {
                case 0:
                    weaponChoiceSelected--;
                    break;
                case 1:
                    weaponChoiceSelected++;
                    break;
            }
        }
        else
        {
            weaponImage.SetActive(true);
        }
    }

    public void EquipButton()
    {
        print(weaponChoiceSelected);
        switchWeapon.weaponSelected = weaponChoiceSelected;
    }

        void DisableAllWeaponImages()
    {
        weapon0.SetActive(false);
        weapon1.SetActive(false);
        weapon2.SetActive(false);
        weapon3.SetActive(false);
        weapon4.SetActive(false);
    }
}
