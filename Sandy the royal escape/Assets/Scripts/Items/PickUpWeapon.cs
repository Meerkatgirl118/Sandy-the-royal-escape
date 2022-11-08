using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    ItemStorage itemStorage;
    PlayerMovement playerMovement;
    Animator animator;

    [SerializeField] string weaponType = "";

    void Start()
    {
        itemStorage = FindObjectOfType<ItemStorage>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        animator = playerMovement.GetComponentInChildren<Animator>();
    }

     void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            AddWeaponToInventory();
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            AddWeaponToInventory();
        }
    }

    void AddWeaponToInventory()
    {
        switch (weaponType)
        {
            case "knife":
                itemStorage.knifeOwned = true;
                animator.SetTrigger("pickUpItem");
                playerMovement.movementEnabled = false;
                StartCoroutine(WaitToReenable());
                break;
        }
    }
    IEnumerator WaitToReenable()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(GetComponent<MeshRenderer>());
        yield return new WaitForSeconds(1f);
        playerMovement.movementEnabled = true;
        Destroy(gameObject);
    }
}
