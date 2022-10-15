using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Collider myCollider;

    GameObject player;

    [SerializeField] int directionFacing = 0; // (0) - left, (1) - right, (2) - up, (3) - down

    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            PlayerFacingDirection();
        }
    }

    void PlayerFacingDirection()
    {
        switch (directionFacing) 
        {
            case 0: // left
                doorMovement(new Vector3(-4, 0, 0));
                break;
            case 1: // right
                doorMovement(new Vector3(4, 0, 0));
                break;
            case 2: // up
                doorMovement(new Vector3(0, 0, 4));
                break;
            case 3: // down
                doorMovement(new Vector3(0, 0, -4));
                break;
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
