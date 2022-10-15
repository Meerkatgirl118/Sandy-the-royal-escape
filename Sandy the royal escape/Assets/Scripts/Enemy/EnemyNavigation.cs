using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    PlayerMovement playerMovement;
    OpenCloseMenu openCloseMenu;
    NavMeshAgent agent;

    public float enemyAttackRadius = 6f;
    bool touchingPlayer = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        agent = GetComponent<NavMeshAgent>();
        openCloseMenu = FindObjectOfType<OpenCloseMenu>();
    }

    void Update()
    {
        if (openCloseMenu.isMenuOpen)
        {
            agent.enabled = false;
        }
        else if (openCloseMenu.menuOpenCooldownActive)
        {
            agent.enabled = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            agent.enabled = false;
            touchingPlayer = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            agent.enabled = true;
            touchingPlayer = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyAttackRadius);

    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(playerMovement.transform.position, this.transform.position);

        if (distanceToPlayer < enemyAttackRadius && !touchingPlayer && !openCloseMenu.isMenuOpen && agent != null)
        {
            agent.destination = playerMovement.transform.position;
        }
    }
}
