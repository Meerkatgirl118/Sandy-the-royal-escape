using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    OverworldUI overworldUI;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        overworldUI = FindObjectOfType<OverworldUI>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0))
        {
            overworldUI.DisplayAttackUI();
            collision.gameObject.GetComponent<EnemyBehaviour>().myHealth -= 10;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            overworldUI.HideDisplayAttackUI();
        }
    }
}
