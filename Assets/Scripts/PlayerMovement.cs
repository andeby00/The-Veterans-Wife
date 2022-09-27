using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float movSpeed = 100f;
    [SerializeField] float jumpHeight = 100f;
    private readonly float MOVE_CONSTANT = 50f; 

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        _rb.velocity = new Vector3(horizontal * movSpeed * MOVE_CONSTANT * Time.deltaTime, _rb.velocity.y, vertical * movSpeed * MOVE_CONSTANT * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight, _rb.velocity.z);
    }
}
