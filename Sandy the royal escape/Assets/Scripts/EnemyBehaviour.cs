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
        Debug.Log("Collision general");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with Sandy");
            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        playerMovement.movementEnabled = false;
        playerStats.playerCurrentHealth -= 10;
        Wait();
        playerMovement.movementEnabled = true;
    }

    void EnemyDefeatTrigger()
    {
        if (enemyType == "cleaningRat" && myHealth <= 0) { print("enemy defeated"); EnemyDefeat(); }
        if (enemyType == "cookingRat" && myHealth <= 0) { print("enemy defeated"); EnemyDefeat(); }
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
}
