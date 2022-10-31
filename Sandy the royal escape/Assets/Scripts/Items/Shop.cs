using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class Shop : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    OpenCloseMenu openCloseMenu;
    ItemStorage itemStorage;
    
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject freelookCamera;
    [SerializeField] TMP_Text itemCostText;

    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    [SerializeField] GameObject item4;

    public string[] itemsInShop;
    public int[] itemCosts;
    public int totalItemsForSale;
    public int shopItemSelected;
    public bool shopActive = false;

    public string currentShopItem;

    public string typeOfItem; // "weapon", "heart"
    public int weaponForSale;

    void Start()
    {
        GetScripts();
        totalItemsForSale = itemsInShop.Length - 1;
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return) && !shopActive)
        {
            ActivateShop();
        }
        else if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Escape) && shopActive)
        {
            DeactivateShop();
        }
    }

    void Update()
    {
        DisplayItem();
        MatchItem();
    }

    void DisplayItem()
    {
        switch (shopItemSelected)
        {
            case 0:
                SetAllItemsInactive();
                item1.gameObject.SetActive(true);
                break;
            case 1:
                SetAllItemsInactive();
                item2.gameObject.SetActive(true);
                break;
            case 2:
                SetAllItemsInactive();
                item3.gameObject.SetActive(true);
                break;
            case 3:
                SetAllItemsInactive();
                item4.gameObject.SetActive(true);
                break;
        }
        itemCostText.text = "X " + itemCosts[shopItemSelected].ToString();
    }
    void SetAllItemsInactive()
    {
        item1.gameObject.SetActive(false);
        item2.gameObject.SetActive(false);
        item3.gameObject.SetActive(false);
        item4.gameObject.SetActive(false);
    }
    public void NextItem()
    {
        if (shopItemSelected < totalItemsForSale)
        {
            shopItemSelected += 1;
        }
        else
        {
            shopItemSelected = 0;
        }
    }

    public void PreviousItem()
    {
        if (shopItemSelected <= 0)
        {
            shopItemSelected = totalItemsForSale;
        }
        else
        {
            shopItemSelected -= 1;
        }
    }

    public void Buy()
    {
        if (itemStorage.glassSlipperAmount >= itemCosts[shopItemSelected])
        {
            MatchItem();
            BuyItem();
            itemStorage.glassSlipperAmount -= itemCosts[shopItemSelected];
        }
    }

    void MatchItem()
    {
        currentShopItem = itemsInShop[shopItemSelected];

        for (int i = 0; i < itemStorage.weapons.Length; i++)
        {
            if (currentShopItem == itemStorage.weapons[i])
            {
                typeOfItem = "weapon";
                weaponForSale = i;
                break;
            }
            else
            {
                typeOfItem = "heart";
            }
        }
    }

    void BuyItem()
    {
        if (typeOfItem == "weapon")
        {
            switch (weaponForSale)
            {
                case 1:
                    itemStorage.knifeOwned = true;
                    itemStorage.weaponsAvailable.Add("knife");
                    break;
                case 2:
                    itemStorage.whipOwned = true;
                    itemStorage.weaponsAvailable.Add("whip");
                    break;
                case 3:
                    itemStorage.swordOwned = true;
                    itemStorage.weaponsAvailable.Add("sword");
                    break;
                case 4:
                    itemStorage.axeOwned = true;
                    itemStorage.weaponsAvailable.Add("axe");
                    break;
            }
        }
        else if (typeOfItem == "heart")
        {
            playerStats.playerCurrentHealth += 40;
        }
    }

    void ActivateShop()
    {
        shopUI.SetActive(true);
        freelookCamera.SetActive(false);
        shopActive = true;
        playerMovement.movementEnabled = false;
        openCloseMenu.canOpenMenu = false;
        openCloseMenu.isMenuOpen = true;
        openCloseMenu.menuOpenCooldownActive = true;
        StartCoroutine(MenuOpenPause());
    }

    void DeactivateShop()
    {
        shopUI.SetActive(false);
        freelookCamera.SetActive(true);
        shopActive = false;
        playerMovement.movementEnabled = true;
        openCloseMenu.canOpenMenu = true;
        openCloseMenu.isMenuOpen = false;
        openCloseMenu.menuOpenCooldownActive = true;
        StartCoroutine(MenuOpenPause());
    }

    void GetScripts()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
        itemStorage = FindObjectOfType<ItemStorage>();
        playerStats = FindObjectOfType<PlayerStats>();
        freelookCamera = FindObjectOfType<Cinemachine.CinemachineFreeLook>().gameObject;
    }
    IEnumerator MenuOpenPause()
    {
        yield return new WaitForSeconds(openCloseMenu.menuOpenCooldownTime);
        openCloseMenu.menuOpenCooldownActive = false;
    }
}
