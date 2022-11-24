using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerStats playerStats;
    EnemyStats enemyStats;
    PlayerMovement playerMovement;
    OverworldUI overworldUI;
    OpenCloseMenu openCloseMenu;
    ItemStorage itemStorage;
    Animator animator;

    public bool attackCooldownActive = false;
    public float attackCooldownTime = 2f;
    public int playerAttack;
    public int weaponAttackBoost;


    Vector3 relativePosition;
    Quaternion targetRotation;
    public float speed = 1f;
    Quaternion currentRotation;

    bool isRotating = false;

    public float rotationTime = 0.0f;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        overworldUI = FindObjectOfType<OverworldUI>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
        itemStorage = FindObjectOfType<ItemStorage>();
        animator = GetComponentInChildren<Animator>();
        playerAttack = playerStats.playerAttack;
    }

    void CheckPlayerAttack()
    {
        switch (itemStorage.weaponInUse)
        {
            case 0:
                weaponAttackBoost = itemStorage.fistStrengthBoost;
                animator.SetBool("attack0", true);
                break;
            case 1:
                weaponAttackBoost = itemStorage.knifeStrengthBoost;
                animator.SetBool("attack1", true);
                break;
            case 2:
                weaponAttackBoost = itemStorage.whipStrengthBoost;
                animator.SetBool("attack2", true);
                break;
            case 3:
                weaponAttackBoost = itemStorage.swordStrengthBoost;
                animator.SetBool("attack3", true);
                break;
            case 4:
                weaponAttackBoost = itemStorage.axeStrengthBoost;
                animator.SetBool("attack4", true);
                break;
        }
        playerAttack = playerStats.playerAttack + weaponAttackBoost;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && Input.GetAxis("Fire1") == 1 && !openCloseMenu.isMenuOpen && playerMovement.movementEnabled && !attackCooldownActive)
        {
            CheckPlayerAttack();
            PlayerFaceEnemy(collision.transform);
            
            animator.SetBool("isAttacking", true);
            playerMovement.movementEnabled = false;
            overworldUI.EnemyHealthUI(collision.gameObject.GetComponent<EnemyBehaviour>().myHealth, collision.gameObject);
            if (!attackCooldownActive)
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().myHealth -= playerAttack;
            }
            overworldUI.DisplayAttackUI(collision.gameObject);
            attackCooldownActive = true;
            overworldUI.EnemyHealthUI(collision.gameObject.GetComponent<EnemyBehaviour>().myHealth, collision.gameObject);

            playerMovement.movementEnabled = true;
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


    void StopAttack()
    {
        switch (itemStorage.weaponInUse)
        {
            case 0:
                animator.SetBool("attack0", false);
                break;
            case 1:
                animator.SetBool("attack1", false);
                break;
            case 2:
                animator.SetBool("attack2", false);
                break;
            case 3:
                animator.SetBool("attack3", false);
                break;
            case 4:
                animator.SetBool("attack4", false);
                break;
        }
        playerAttack = playerStats.playerAttack + weaponAttackBoost;
    } 

    void PlayerFaceEnemy(Transform enemyPosition)
    {
        transform.LookAt(enemyPosition);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        animator.SetBool("isAttacking", false);
        StopAttack();
        attackCooldownActive = false;
    }
}
