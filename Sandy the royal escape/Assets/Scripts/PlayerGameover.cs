using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGameover : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    [SerializeField] GameObject gameOverRed;

    int currentScene;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (playerStats.playerCurrentHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BelowGround")
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        playerMovement.movementEnabled = false;
        gameOverRed.SetActive(true);
        yield return new WaitForSeconds(5f);
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
