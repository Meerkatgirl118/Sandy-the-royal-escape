using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerGameover : MonoBehaviour
{
    PlayerStats playerStats;
    OpenCloseMenu openCloseMenu;
    PlayerMovement playerMovement;
    PlayerRotate playerRotate;

    [SerializeField] GameObject gameOverRed;

    public bool gameoverTriggered = false;
    [SerializeField] int currentScene;
    [SerializeField] GameObject attackUI;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerRotate = FindObjectOfType<PlayerRotate>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
    }

    void Update()
    {
        if (playerStats.playerCurrentHealth <= 0 && !gameoverTriggered)
        {
            playerStats.playerCurrentHealth = 0;
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
        playerStats.playerCurrentHealth = 0;
        gameoverTriggered = true;
        openCloseMenu.isMenuOpen = true;
        openCloseMenu.canOpenMenu = false;

        attackUI.SetActive(false);
        FindObjectOfType<CinemachineFreeLook>().enabled = false;
        playerMovement.movementEnabled = false;

        playerMovement.gameObject.GetComponentInChildren<Animator>().SetBool("isWalking", false);
        playerMovement.gameObject.GetComponentInChildren<Animator>().SetBool("isRunning", false);
        playerMovement.gameObject.GetComponentInChildren<Animator>().SetBool("isJumping", false);
        playerMovement.gameObject.GetComponentInChildren<Animator>().SetTrigger("defeat");

        playerMovement.enabled = false;
        playerRotate.enabled = false;
        yield return new WaitForSeconds(10f);
        gameOverRed.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }

    public void ExitGame()
    {
        Application.Quit();
        print("Quit");
    }
}
