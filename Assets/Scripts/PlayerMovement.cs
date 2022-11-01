using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float movSpeed = 10f;
    [SerializeField] float maxSpeed = 100f;
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

        // Vector3 move = transform.right * horizontal + transform.forward * vertical;
        // controller.Move(move * movSpeed * Time.deltaTime);
        //
        // _velocity.y += gravity * Time.deltaTime;
        // controller.Move(_velocity * Time.deltaTime);

        //Vector3 vel = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        Vector3 wishDir = transform.right * horizontal + transform.forward * vertical;

        //float currentSpeed = Vector3.Dot(vel, wishDir);
        
        Vector3 move = wishDir * (movSpeed * Time.deltaTime);
        // Vector3 move = vel + wishDir * ((movSpeed - currentSpeed) * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        move.y = _velocity.y * Time.deltaTime;
        controller.Move(move);
        

        
        // _rb.velocity = new Vector3(horizontal * movSpeed * Time.deltaTime, _rb.velocity.y, vertical * movSpeed * Time.deltaTime);

        if (IsGrounded() && _velocity.y < 0)
        {
            _velocity.y = 0; // -2f
        }
        
        if (Input.GetButtonDown("Jump") && IsGrounded())    
        {
            Jump();
        }

        //velocityDiplay.SetText(controller.velocity.ToShortString());
        
        //velocityDiplay.SetText(vel.ToShortString() + " - " + wishDir.ToShortString());
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
