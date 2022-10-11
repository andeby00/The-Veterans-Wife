using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float movSpeed = 100f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] CharacterController controller;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] TextMeshProUGUI velocityDiplay;

    float _groundDistance = 0.1f;
    Vector3 _velocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * movSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
        

        // _rb.velocity = new Vector3(horizontal * movSpeed * Time.deltaTime, _rb.velocity.y, vertical * movSpeed * Time.deltaTime);

        if (IsGrounded() && _velocity.y < 0)
        {
            _velocity.y = 0f; // -2f
        }
        
        if (Input.GetButtonDown("Jump") && IsGrounded())    
        {
            Jump();
        }

        velocityDiplay.SetText(transform.forward.ToShortString());//// skal testes, tror ik virker dog 
        
        
        
        // velocityDiplay.SetText(Mathf.Sqrt(Mathf.Pow(controller.velocity.x, 2) + Mathf.Pow(controller.velocity.z, 2)).ToShortString());
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, _groundDistance, ground);
    }

    void Jump()
    {
        _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        //controller.Move(_velocity);
        // _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight, _rb.velocity.z);
    }
}
