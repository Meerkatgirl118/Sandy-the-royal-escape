using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSlipperCollectable : MonoBehaviour
{
    ItemStorage itemStorage;
    PlayerMovement playerMovement;
    Animator animator;

    void Start()
    {
        itemStorage = FindObjectOfType<ItemStorage>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = playerMovement.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            PickUpItem();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            PickUpItem();
        }
    }
    void PickUpItem()
    {
        playerMovement.movementEnabled = false;
        animator.SetTrigger("pickUpItem");
        itemStorage.glassSlipperAmount++;
        StartCoroutine(WaitToReenable());
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
