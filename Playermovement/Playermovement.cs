using System.Collections;
using Sytem.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight; 
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalinput;
    float verticalinput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void start()
    
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    private void update()
   
    {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    
        MyInput();

        if(grounded)
        rb.drag = groundDrag;
        else
        rb.drag = 0
    }
   
    private void FixedUpdate()
   
    {
        MovePlayer();
    }
    
     private void Myinput()
    
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

     private void MovePlayer()
   
    {
        moveDirection = orientation forward * verticalInput + orientation.right * horizontal input;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void Speedcontrol()
    {
    Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }
}

