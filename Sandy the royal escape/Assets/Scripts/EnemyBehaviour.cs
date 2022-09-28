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

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        overworldUI = FindObjectOfType<OverworldUI>();
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
        if (enemyType == "cleaningRat" && enemyStats.cleaningRatHealth <= 0) { print("enemy defeated"); EnemyDefeat(); }
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
