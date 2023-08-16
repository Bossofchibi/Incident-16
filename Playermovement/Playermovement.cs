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
    public float crouchYScale;
    private float startYScale;

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
   
    [Header("Ground Check")]
    public float playerHeight; 
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope; 

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
        crouching,
        air
    }

    private void start()

    startYScale = transform.localScale.y;
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

        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if(Input.GetkeyUp(crouch))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
   {
        if (Input.GetKey(crouchkey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        
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

        if(OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * modeSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            else if(!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f  * airMultiplier, ForceMode.Force);

            rb.useGravity = !OnSlope();
    }

    private void Speedcontrol()
    {

        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
             Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(flatvel.magnitude > moveSpeed)
            {
            Vector3 imitedVel = flatVel/normalized * moveSpeed
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void jump()
    {
        exitingSlope = true;
        
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAnlge && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjecOnPlane(moveDirection, slopeHit.normal).Normalized;
    }
        

}


