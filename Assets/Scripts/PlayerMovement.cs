using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpHeight;

    private Vector3 velocity;
    private Vector3 moveDirection;

    private float horizontal;
    private float vertical;

    private void Update()
    {
        GetInput();
        Gravity();
        Move();
        Jump();
    }

    private void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void Gravity()
    {
        //Verifica se o Jogador esta caindo
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

    private void Move()
    {
        /*
        moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection.Normalize();

        Vector3 movement = moveDirection * moveSpeed;
        movement.y = velocity.y;

        controller.Move(movement * Time.deltaTime);
        */
        
        Vector3 movement = new Vector3(horizontal * moveSpeed, velocity.y, vertical * moveSpeed);
        movement = Camera.main.transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
