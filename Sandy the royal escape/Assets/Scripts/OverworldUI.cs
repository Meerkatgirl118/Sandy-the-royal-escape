using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverworldUI : MonoBehaviour
{
    [SerializeField] GameObject overworldUI;
    [SerializeField] TMP_Text playerHealthUI;
    [SerializeField] TMP_Text attackUI;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;

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
        HeartUI();
    }

    public void DisplayAttackUI()
    {
        attackUI.gameObject.SetActive(true);
    }
    public void HideDisplayAttackUI()
    {
        attackUI.gameObject.SetActive(false);
    }
    void HeartUI()
    {
        if (playerStats.playerCurrentHealth <= 0) { heart3.SetActive(false); }
        else if (playerStats.playerCurrentHealth <= 40) { heart2.SetActive(false); }
        else if (playerStats.playerCurrentHealth <= 80) { heart1.SetActive(false); }

        if (playerStats.playerCurrentHealth > 80) { heart1.SetActive(true); }
        else if (playerStats.playerCurrentHealth > 40) { heart2.SetActive(true); }
        else if (playerStats.playerCurrentHealth > 0) { heart3.SetActive(true); }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
