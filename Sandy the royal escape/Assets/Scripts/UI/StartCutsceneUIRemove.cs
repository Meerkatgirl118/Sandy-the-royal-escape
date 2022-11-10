using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutsceneUIRemove : MonoBehaviour
{
    [SerializeField] GameObject startCutsceneUI;
    [SerializeField] float startCutsceneLength = 15f;

    void Update()
    {
        StartCoroutine(RemoveStartCutsceneUI());
    }
    public IEnumerator RemoveStartCutsceneUI()
    {
        yield return new WaitForSeconds(startCutsceneLength);
        Destroy(startCutsceneUI);
        Destroy(this);
    }
}
