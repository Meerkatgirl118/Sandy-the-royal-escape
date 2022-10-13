using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    PlayerMovement playerMovement;
    OverworldUI overworldUI;
    OpenCloseMenu openCloseMenu;

    [SerializeField] string enemyTypeString = "";

    public int myHealth = 20000;
    public int myAttack = 40;
    public float myAttackCooldownAmount = 5f;
    [SerializeField] bool myAttackCooldownActive = false;

    void Start()
    {
        FindNeededScripts();
        DefineEnemyStats();
    }

    void Update()
    {
        EnemyDefeatTrigger();
    }

    void FindNeededScripts()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        overworldUI = FindObjectOfType<OverworldUI>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
    }
    void DefineEnemyStats()
    {
        switch (enemyTypeString)
        {
            case "cleaningRat":
                myHealth = enemyStats.cleaningRatHealth; 
                myAttack = enemyStats.cleaningRatAttack;
                break;
            case "cookingRat":
                myHealth = enemyStats.cookingRatHealth; 
                myAttack = enemyStats.cookingRatAttack;
                break;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive && !openCloseMenu.isMenuOpen)
        {
            StartCoroutine(AttackCooldown());
            EnemyAttack(myAttack);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive && !openCloseMenu.isMenuOpen)
        {
            StartCoroutine(AttackCooldown());
            EnemyAttack(myAttack);
        }
    }

    void EnemyAttack(int myAttackStat)
    {
        playerMovement.movementEnabled = false;
        playerStats.playerCurrentHealth -= myAttack;
        StartCoroutine(Wait());
        playerMovement.movementEnabled = true;
    }

    void EnemyDefeatTrigger()
    {
        if (myHealth <= 0) 
        { 
            Wait(); 
            EnemyDefeat(); }
    }

    void EnemyDefeat()
    {
        overworldUI.HideDisplayAttackUI();
        Destroy(this.gameObject);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator AttackCooldown()
    {
        myAttackCooldownActive = true;
        yield return new WaitForSeconds(myAttackCooldownAmount);
        myAttackCooldownActive = false;
    }
}
