using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    void Start()
    {
        
    }

    public void Resume()
    {
        SceneManager.LoadScene(0);
    }

    public void Chapter1()
    {
        SceneManager.LoadScene(0);
    }
    public void Chapter2()
    {
        
    }
    public void Chapter3()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
