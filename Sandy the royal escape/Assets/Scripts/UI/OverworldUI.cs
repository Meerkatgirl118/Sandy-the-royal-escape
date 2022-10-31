using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverworldUI : MonoBehaviour
{
    [SerializeField] GameObject overworldUI;
    [SerializeField] TMP_Text playerHealthUI;
    [SerializeField] TMP_Text attackUI;
    [SerializeField] TMP_Text enemyHealthUI;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;

    [SerializeField] TMP_Text glassSlipperAmountUI;

    PlayerStats playerStats;
    EnemyStats enemyStats;
    ItemStorage itemStorage;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        itemStorage = FindObjectOfType<ItemStorage>();
    }


    void Update()
    {
        UIHealthDisplay();
        HeartUI(playerStats.playerCurrentHealth);
    }

    void UIHealthDisplay()
    {
        playerHealthUI.text = "Health: " + playerStats.playerCurrentHealth;
        glassSlipperAmountUI.text = "X" + itemStorage.glassSlipperAmount;

    }
    public void DisplayAttackUI(GameObject enemy)
    {
        attackUI.gameObject.SetActive(true);
    }
    public void HideDisplayAttackUI()
    {
        attackUI.gameObject.SetActive(false);
    }
    public void EnemyHealthUI(int enemyCurrentHealth, GameObject enemy)
    {
        switch (enemyCurrentHealth)
        {
            case > 0:
                enemyHealthUI.text = "HP: " + enemyCurrentHealth;
                break;
            case <= 0:
                enemyHealthUI.text = "HP: " + 0;
                break;
        }
    }

    void HeartUI(int playerHealth)
    {
        switch (playerHealth)
        {
            case <= 0:
                heart3.SetActive(false);
                break;
            case <= 40:
                heart2.SetActive(false);
                break;
            case <= 80:
                heart1.SetActive(false);
                break;
        }
        switch (playerHealth)
        {
            case > 80:
                heart1.SetActive(true);
                break;
            case > 40:
                heart2.SetActive(true);
                break;
            case > 0:
                heart3.SetActive(true);
                break;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

}
