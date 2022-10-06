using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    PlayerMovement playerMovement;
    NavMeshAgent agent;

    bool touchingPlayer = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        agent = GetComponent<NavMeshAgent>();
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

    void FixedUpdate()
    {
        if (!touchingPlayer)
        {
            agent.destination = playerMovement.transform.position;
        }
    }
}
