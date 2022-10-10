using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    OpenCloseMenu openCloseMenu;

    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject equipMenuUI;

    public bool equipMenuOpen = false;

    void Start()
    {
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
    }

    void Update()
    {
        if (openCloseMenu.subMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
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
