using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    PlayerMovement playerMovement;
    OverworldUI overworldUI;

    [SerializeField] string enemyType = "";
    public int myHealth = 20000;
    public float myAttackCooldownAmount = 5f;
    [SerializeField] bool myAttackCooldownActive = false;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        overworldUI = FindObjectOfType<OverworldUI>();
        if (enemyType == "cleaningRat") { myHealth = enemyStats.cleaningRatHealth; }
        if (enemyType == "cookingRat") { myHealth = enemyStats.cookingRatHealth; }
    }

    void Update()
    {
        EnemyDefeatTrigger();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive)
        {
            StartCoroutine(AttackCooldown());
            EnemyAttack();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive)
        {
            StartCoroutine(AttackCooldown());
            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        playerMovement.movementEnabled = false;
        if (enemyType == "cleaningRat") { playerStats.playerCurrentHealth -= enemyStats.cleaningRatAttack; }
        else if (enemyType == "cookingRat") { playerStats.playerCurrentHealth -= enemyStats.cookingRatAttack; }
        Wait();
        playerMovement.movementEnabled = true;
    }

    void EnemyDefeatTrigger()
    {
        if (myHealth <= 0) { Wait(); print("enemy defeated"); EnemyDefeat(); }
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
