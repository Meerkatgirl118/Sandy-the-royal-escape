using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerRotate playerRotate;

    CharacterController characterController;
    Animator animator;
    [SerializeField] GameObject sandyModel;

    [SerializeField] float walkSpeed = 80f;
    [SerializeField] float runSpeed = 100f;

    public float gravity = 9.8f;
    public float jumpHeight = 5f;
    Vector3 movementDirection = Vector3.zero;

    public bool movementEnabled = true;

    public float RotateSpeed = 30f;

    void Start()
    {
        characterController = GetComponentInChildren<CharacterController>();
        playerRotate = FindObjectOfType<PlayerRotate>();
        animator = GetComponentInChildren<Animator>();
    }


    void FixedUpdate()
    {
        WalkAndRun();
        Jump();
        CheckIfGrounded();
    }
    void WalkAndRun()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        move = this.transform.TransformDirection(move);
        if (move != new Vector3(0, 0, 0) && movementEnabled == true)
        {
            if (Input.GetAxis("Fire3") == 1) // Fire3 is left shift on keyboard
            {
                animator.SetBool("isRunning", true);
                characterController.SimpleMove(move * Time.deltaTime * runSpeed);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                characterController.SimpleMove(move * Time.deltaTime * walkSpeed);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", false);
        }
    }

    void CheckIfGrounded()
    {
        if (characterController.isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }
    void Jump()
    {
        if (characterController.isGrounded && Input.GetAxis("Jump") == 1 && movementEnabled == true)
        {
            animator.SetBool("isJumping", true);
            movementDirection.y = jumpHeight;
        }
        movementDirection.y -= gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime);
    }
}
