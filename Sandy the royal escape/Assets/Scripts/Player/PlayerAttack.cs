using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    OverworldUI overworldUI;

    public bool attackCooldownActive = false;
    public float attackCooldownTime = 2f;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        overworldUI = FindObjectOfType<OverworldUI>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && Input.GetAxis("Fire1") == 1)
        {
            overworldUI.EnemyHealthUI(collision.gameObject.GetComponent<EnemyBehaviour>().myHealth, collision.gameObject);
            if (!attackCooldownActive)
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().myHealth -= 10;
            }
            overworldUI.DisplayAttackUI(collision.gameObject);
            attackCooldownActive = true;
            StartCoroutine(AttackCooldown());
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            overworldUI.HideDisplayAttackUI();
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        attackCooldownActive = false;
    }
}
