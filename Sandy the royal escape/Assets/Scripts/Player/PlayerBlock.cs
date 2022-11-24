using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    EnemyBehaviour enemyBehaviour;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Animator animator;

    float blockTime = 5f;
    public bool playerIsBlocking = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Block());
        }
    }

    IEnumerator Block()
    {
        playerMovement.movementEnabled = false;
        animator.SetBool("isBlocking", true);
        playerIsBlocking = true;
        yield return new WaitForSeconds(blockTime);
        playerIsBlocking = false;
        playerMovement.movementEnabled = true;
        animator.SetBool("isBlocking", false);
    }
}
