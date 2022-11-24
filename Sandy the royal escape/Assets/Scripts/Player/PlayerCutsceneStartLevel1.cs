using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCutsceneStartLevel1 : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator playerAnimator;
    GameObject mainCamera;
    [SerializeField] GameObject cutsceneCamera;

    [SerializeField] Canvas startCutsceneUI;
    [SerializeField] GameObject overworldUI;
    Animator cutsceneUIAnim;

    [SerializeField] float startCutsceneLength = 10f;
    [SerializeField] float cutsceneFadeLength = 3f;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cutsceneUIAnim = startCutsceneUI.gameObject.GetComponent<Animator>();
        overworldUI.SetActive(false);

        cutsceneCamera.SetActive(true);
        playerMovement.movementEnabled = false;
        mainCamera.SetActive(false);
        StartCoroutine(WaitForCutsceneEnd());
    }

    IEnumerator WaitForCutsceneEnd()
    {
        yield return new WaitForSeconds(startCutsceneLength);
        StartCoroutine(CutsceneStop());
    }
    IEnumerator CutsceneStop()
    {
        startCutsceneUI.gameObject.SetActive(true);
        cutsceneUIAnim.SetTrigger("cutsceneFade");
        yield return new WaitForSeconds(cutsceneFadeLength);
        cutsceneCamera.SetActive(false);
        playerMovement.movementEnabled = true;
        mainCamera.SetActive(true);
        overworldUI.SetActive(true);
        Destroy(gameObject);
    }
}
