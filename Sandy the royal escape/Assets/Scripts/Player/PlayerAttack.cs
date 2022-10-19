using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    OverworldUI overworldUI;
    OpenCloseMenu openCloseMenu;
    ItemStorage itemStorage;

    public bool attackCooldownActive = false;
    public float attackCooldownTime = 2f;
    public int playerAttack;
    public int weaponAttackBoost;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        overworldUI = FindObjectOfType<OverworldUI>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
        itemStorage = FindObjectOfType<ItemStorage>();
        playerAttack = playerStats.playerAttack;
    }

    void FixedUpdate()
    {
        
    }

    void CheckPlayerAttack()
    {
        switch (itemStorage.weaponInUse)
        {
            case 0:
                weaponAttackBoost = itemStorage.fistStrengthBoost;
                break;
            case 1:
                weaponAttackBoost = itemStorage.knifeStrengthBoost;
                break;
            case 3:
                weaponAttackBoost = itemStorage.whipStrengthBoost;
                break;
            case 4:
                weaponAttackBoost = itemStorage.swordStrengthBoost;
                break;
            case 5:
                weaponAttackBoost = itemStorage.axeStrengthBoost;
                break;
        }
        playerAttack = playerStats.playerAttack + weaponAttackBoost;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && Input.GetAxis("Fire1") == 1 && !openCloseMenu.isMenuOpen)
        {
            CheckPlayerAttack();

            overworldUI.EnemyHealthUI(collision.gameObject.GetComponent<EnemyBehaviour>().myHealth, collision.gameObject);
            if (!attackCooldownActive)
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().myHealth -= playerAttack;
            }
            overworldUI.DisplayAttackUI(collision.gameObject);
            attackCooldownActive = true;
            overworldUI.EnemyHealthUI(collision.gameObject.GetComponent<EnemyBehaviour>().myHealth, collision.gameObject);

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
