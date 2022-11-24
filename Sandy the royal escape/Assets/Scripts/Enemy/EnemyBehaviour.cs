using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Define scripts
    [SerializeField] PlayerStats playerStats;
    [SerializeField] EnemyStats enemyStats;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] OverworldUI overworldUI;
    [SerializeField] OpenCloseMenu openCloseMenu;
    [SerializeField] Animator animator;
    [SerializeField] PlayerBlock playerBlock;
    [SerializeField] Animator playerAnimator;
    [SerializeField] BlindMiceBoss blindMiceBoss;

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

    public void FindNeededScripts()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        overworldUI = FindObjectOfType<OverworldUI>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
        animator = GetComponentInChildren<Animator>();
        playerBlock = FindObjectOfType<PlayerBlock>();
        playerAnimator = FindObjectOfType<PlayerMovement>().GetComponentInChildren<Animator>();
        blindMiceBoss = FindObjectOfType<BlindMiceBoss>();
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
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive && !openCloseMenu.isMenuOpen && !enemyDefeatTriggered && !playerBlock.playerIsBlocking)
        {
            StartCoroutine(AttackCooldown(myAttackCooldownAmount));
            EnemyAttack(myAttack);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !myAttackCooldownActive && !openCloseMenu.isMenuOpen && !enemyDefeatTriggered && !playerBlock.playerIsBlocking)
        {
            StartCoroutine(AttackCooldown(myAttackCooldownAmount));
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
        playerAnimator.SetBool("isAttacking", false);
        playerMovement.movementEnabled = false;
        playerStats.playerCurrentHealth -= myAttack;
        StartCoroutine(Wait());
        playerAnimator.SetTrigger("takeDamage");
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

    public IEnumerator AttackCooldown(float cooldownLength)
    {
        animator.SetBool("isAttacking", false);
        myAttackCooldownActive = true;
        yield return new WaitForSeconds(cooldownLength);
        myAttackCooldownActive = false;
    }

    IEnumerator DestroyAfterDefeat()
    {
        GetComponent<EnemyNavigation>().enabled = false;
        myAttackCooldownActive = true;
        yield return new WaitForSeconds(myDefeattime);
        if (blindMiceBoss.ratAttackSectionTriggered)
        {
            blindMiceBoss.enemiesDefeated++;
        }
        Destroy(this.gameObject);
    }
}
