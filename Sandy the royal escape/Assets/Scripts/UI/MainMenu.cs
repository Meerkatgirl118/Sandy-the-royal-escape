using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
    OpenCloseMenu openCloseMenu;
    [SerializeField] CinemachineFreeLook virtualCamera;

    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject equipMenuUI;

    public bool equipMenuOpen = false;


    void Start()
    {
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
        virtualCamera = FindObjectOfType<CinemachineFreeLook>();
    }

    void Update()
    {
        if (openCloseMenu.subMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            virtualCamera.enabled = true;
            CloseSubMenus();
        }
    }

    void CloseSubMenus()
    {
        equipMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        openCloseMenu.subMenuOpen = false;
    }
    public void Equip()
    {
        openCloseMenu.subMenuOpen = true;
        equipMenuOpen = true;
        mainMenuUI.SetActive(false);
        equipMenuUI.SetActive(true);
        virtualCamera.enabled = false;
    }
    public void Save()
    {
        // Save
    }
    public void Exit()
    {
        Application.Quit();
    }
}
