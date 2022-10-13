using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Collider myCollider;

    GameObject player;

    // (-2, 0, 0) - left, (2, 0, 0) - right, (0, 2, 0) - up, (0, -2, 0) - down

    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            doorMovement(new Vector3(-2,0,0));
        }
    }

    void doorMovement(Vector3 movementDirection)
    {
        this.myCollider.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position += movementDirection;
        player.GetComponent<CharacterController>().enabled = true;
        this.myCollider.enabled = true;
    }
}
