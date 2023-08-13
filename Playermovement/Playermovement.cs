using System.Collections;
using Sytem.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    [Header("Movement")]
    public float moveSpeed;

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
    MyInput();
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
}
