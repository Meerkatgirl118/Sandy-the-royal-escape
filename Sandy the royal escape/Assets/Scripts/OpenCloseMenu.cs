using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseMenu : MonoBehaviour
{
    [SerializeField] GameObject overworldUI;
    [SerializeField] GameObject menuUI;
    PlayerMovement playerMovement;

    public bool isMenuOpen = false;
    public bool canOpenMenu = true;
    public bool menuOpenCooldownActive = false;
    public float menuOpenCooldownTime = 0.5f;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetAxis("Cancel") == 1 && !isMenuOpen && canOpenMenu && !menuOpenCooldownActive)
        {
            menuOpenCooldownActive = true;
            MenuOpen();
        }
        if (Input.GetAxis("Cancel") == 1 && isMenuOpen && !menuOpenCooldownActive)
        {
            menuOpenCooldownActive = true;
            MenuClose();
        }
    }
    void MenuOpen()
    {
        playerMovement.movementEnabled = false;
        isMenuOpen = true;
        menuUI.SetActive(true);
        StartCoroutine(MenuOpenPause());
    }
    void MenuClose()
    {
        playerMovement.movementEnabled = true;
        isMenuOpen = false;
        menuUI.SetActive(false);
        StartCoroutine(MenuOpenPause());
    }

    IEnumerator MenuOpenPause()
    {
        yield return new WaitForSeconds(menuOpenCooldownTime);
        menuOpenCooldownActive = false;
    }
}
