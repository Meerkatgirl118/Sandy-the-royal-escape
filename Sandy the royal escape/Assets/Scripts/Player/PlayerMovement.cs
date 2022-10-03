using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float walkSpeed = 80f;
    [SerializeField] float runSpeed = 100f;

    public float gravity = 9.8f;
    public float jumpHeight = 5f;
    Vector3 movementDirection = Vector3.zero;

    public bool movementEnabled = true;

    public float RotateSpeed = 30f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        Rotation();
        WalkAndRun();
        Jump();
    }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }
    }
    void WalkAndRun()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move = this.transform.TransformDirection(move);
        if (move != new Vector3(0, 0, 0) && movementEnabled == true)
        {
            if (Input.GetAxis("Fire3") == 1)
            {
                characterController.SimpleMove(move * Time.deltaTime * runSpeed);
            }
            else
            {
                characterController.SimpleMove(move * Time.deltaTime * walkSpeed);
            }
        }
    }
    void Jump()
    {
        if (characterController.isGrounded && Input.GetAxis("Jump") == 1 && movementEnabled == true)
        {
            movementDirection.y = jumpHeight;
        }
        movementDirection.y -= gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime);
    }
}
