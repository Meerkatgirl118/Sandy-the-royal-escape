using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGameover : MonoBehaviour
{
    PlayerStats playerStats;

    int currentScene;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (playerStats.playerCurrentHealth <= 0)
        {
            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BelowGround")
        {
            GameOver();
        }
    }

    void GameOver()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
