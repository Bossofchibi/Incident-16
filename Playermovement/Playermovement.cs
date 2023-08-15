using System.Collections;
using Sytem.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check")]
    public float playerHeight; 
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalinput;
    float verticalinput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void start()
    
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    private void update()
   
    {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    
        MyInput();
        SpeedControl();
        StateHandler();

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

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        if(grounded && Input.GetKey(sprintKey))
        {
        state = MovementState.sprinting;
        moveSpeed = sprintSpeed;
        }

        else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else 
        {
            state = MovementState.air;
        }
    }

     private void MovePlayer()
    {
        moveDirection = orientation forward * verticalInput + orientation.right * horizontal input;
        
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            else if(!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f  * airMultiplier, ForceMode.Force);
    }

    private void Speedcontrol()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatvel.magnitude > moveSpeed)
        {
            Vector3 imitedVel = flatVel/normalized * moveSpeed
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true
    }
}

