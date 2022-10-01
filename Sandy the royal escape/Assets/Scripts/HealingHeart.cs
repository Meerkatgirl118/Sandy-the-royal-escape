using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHeart : MonoBehaviour
{
    PlayerStats playerStats;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerStats.playerCurrentHealth <= playerStats.playerMaxHealth - 40)
            {
                playerStats.playerCurrentHealth += 40;
                Destroy(gameObject);
            }
            else if (playerStats.playerCurrentHealth >= playerStats.playerMaxHealth - 40 && playerStats.playerCurrentHealth != playerStats.playerMaxHealth)
            {
                playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
                Destroy(gameObject);
            }
        }
    }
}
