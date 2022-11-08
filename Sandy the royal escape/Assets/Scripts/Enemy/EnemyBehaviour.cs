using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Define scripts
    PlayerStats playerStats;
    EnemyStats enemyStats;
    PlayerMovement playerMovement;
    OverworldUI overworldUI;
    OpenCloseMenu openCloseMenu;
    Animator animator;

    // Enemy type
    [SerializeField] string enemyTypeString = "";

    // Stats, attack
    public int myHealth = 20000;
    public int myAttack = 40;
    public float myAttackCooldownAmount = 5f;
    public float myDefeattime = 2f;
    [SerializeField] bool myAttackCooldownActive = false;

    public bool enemyDefeatTriggered = false;

    // Animations
    [SerializeField] string attackAnimation;
    [SerializeField] int animationUsed;

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
        animator = GetComponentInChildren<Animator>();
    }
    void DefineEnemyStats()
    {
        switch (enemyTypeString)
        {
            case "cleaningRat":
                // Define stats
                myHealth = enemyStats.cleaningRatHealth; 
                myAttack = enemyStats.cleaningRatAttack;
                DefineEnemyAnimation();
                break;
            case "cookingRat":
                // Define stats
                myHealth = enemyStats.cookingRatHealth; 
                myAttack = enemyStats.cookingRatAttack;
                DefineEnemyAnimation();
                break;
        }
    }

    void DefineEnemyAnimation()
    {
        switch (enemyTypeString)
        {
            case "cleaningRat":
                attackAnimation = "broom";
                animator.SetBool("isAttackingBroom", true);
                animationUsed = Random.Range(0, 1);
                switch (animationUsed)
                {
                    case 0:
                        animator.SetBool("isAttackingBroom", true);
                        break;
                    case 1:
                        animator.SetBool("isAttackingClipboard", true);
                        break;
                }
                break;
            case "cookingRat":
                attackAnimation = "claw";
                animator.SetBool("isAttackingClaw", true);
                animationUsed = Random.Range(0, 1);
                switch (animationUsed)
                {
                    case 0:
                        animator.SetBool("isAttackingPan", true);
                        break;
                    case 1:
                        animator.SetBool("isAttackingLadle", true);
                        break;
                }
                break;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive && !openCloseMenu.isMenuOpen && !enemyDefeatTriggered)
        {
            StartCoroutine(AttackCooldown());
            EnemyAttack(myAttack);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive && !openCloseMenu.isMenuOpen && !enemyDefeatTriggered)
        {
            StartCoroutine(AttackCooldown());
            EnemyAttack(myAttack);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isAttacking", false);
        }
    }
    void EnemyAttack(int myAttackStat)
    {
        animator.SetBool("isAttacking", true);
        playerMovement.movementEnabled = false;
        playerStats.playerCurrentHealth -= myAttack;
        StartCoroutine(Wait());
        playerMovement.movementEnabled = true;
    }

    void EnemyDefeatTrigger()
    {
        if (myHealth <= 0 && !enemyDefeatTriggered) 
        { 
            EnemyDefeat();
        }
    }

    void EnemyDefeat()
    {
        overworldUI.HideDisplayAttackUI();
        enemyDefeatTriggered = true;
        animator.SetTrigger("defeat");
        StartCoroutine(DestroyAfterDefeat());
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

    IEnumerator DestroyAfterDefeat()
    {
        GetComponent<EnemyNavigation>().enabled = false;
        myAttackCooldownActive = true;
        yield return new WaitForSeconds(myDefeattime);
        Destroy(this.gameObject);
    }
}
