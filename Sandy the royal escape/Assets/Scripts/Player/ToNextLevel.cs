using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour
{
    [SerializeField] int currentScene;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene + 1);
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
