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

    float rotationTime;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
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
            PlayerFaceEnemy(collision.transform);
            animator.SetBool("isAttacking", true);
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

    void PlayerFaceEnemy(Transform enemyPosition)
    {
        rotationTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * speed);
        
        relativePosition = enemyPosition.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);

    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        animator.SetBool("isAttacking", false);
        attackCooldownActive = false;
    }
}
