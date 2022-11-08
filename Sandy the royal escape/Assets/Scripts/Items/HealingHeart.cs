using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHeart : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Animator playerAnimator;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAnimator = playerMovement.gameObject.GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            HealPlayer();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            HealPlayer();
        }
    }
    int HealPlayer()
    {
        if (playerStats.playerCurrentHealth <= playerStats.playerMaxHealth - 40)
        {
            playerMovement.movementEnabled = false;
            playerAnimator.SetTrigger("pickUpItem");

            playerStats.playerCurrentHealth += 40;

            StartCoroutine(WaitToReenable());
        }
        else if (playerStats.playerCurrentHealth >= playerStats.playerMaxHealth - 40 && playerStats.playerCurrentHealth != playerStats.playerMaxHealth)
        {
            playerMovement.movementEnabled = false;
            playerAnimator.SetTrigger("pickUpItem");

            playerStats.playerCurrentHealth = playerStats.playerMaxHealth;

            StartCoroutine(WaitToReenable());
        }
        return playerStats.playerCurrentHealth;
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
