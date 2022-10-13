using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    Vector3 relativePosition;
    Quaternion targetRotation;

    public Transform targetRight;
    public Transform targetLeft;
    public Transform targetFront;
    public Transform targetBack;

    public float speed = 0.01f;

    bool isRotating = false;

    float rotationTime;

    void Update()
    {
        PlayerInputRotation();
        if (isRotating)
        {
            rotationTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * speed);
        }
        if (rotationTime > 1)
        {
            isRotating = false;
        }
    }

    void PlayerInputRotation()
    {
        // Right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerRotation(targetRight);
        }
        // Left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerRotation(targetLeft);
        }
        // Front
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerRotation(targetFront);
        }
        // Back
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerRotation(targetBack);
        }
    }

    void PlayerRotation(Transform target)
    {
        relativePosition = target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);
        isRotating = true;
        rotationTime = 0;
    }
}
