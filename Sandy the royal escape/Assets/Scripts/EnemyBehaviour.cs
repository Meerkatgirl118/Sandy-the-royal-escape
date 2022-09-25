using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    PlayerMovement playerMovement;

    public string enemyType = "";

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
