using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverworldUI : MonoBehaviour
{
    [SerializeField] GameObject overworldUI;
    [SerializeField] TMP_Text playerHealthUI;
    [SerializeField] TMP_Text attackUI;

    PlayerStats playerStats;
    EnemyStats enemyStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
    }


    void Update()
    {
        playerHealthUI.text = "Health: " + playerStats.playerCurrentHealth;
    }

    public void DisplayAttackUI()
    {
        attackUI.gameObject.SetActive(true);
    }
    public void HideDisplayAttackUI()
    {
        attackUI.gameObject.SetActive(false);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
