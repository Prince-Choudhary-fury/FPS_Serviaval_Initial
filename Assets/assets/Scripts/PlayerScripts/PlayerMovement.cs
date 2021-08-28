using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;

    private Vector3 moveDirection;

    public bool isMoving;

    public float speed = 5f;
    private float gravity = 20f;

    public float jumpForce = 10f;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    //Moves the player
    void PlayerMove()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        if (moveDirection.z > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(moveDirection);
    }

    //Apply Gravity
    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        playerJump();
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    //to jump
    private void playerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }
    }
}
