using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f, boostSpeed = 100f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed, activeBoostSpeed;
    public float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    
    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    public ParticleSystem flame1, flame2, flame3;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        
    }
    
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);
        
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed,Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        if (Input.GetAxisRaw("Boost") > 0)
        {
            activeBoostSpeed = Mathf.Lerp(1, Input.GetAxisRaw("Boost") * boostSpeed,
                forwardAcceleration * Time.deltaTime);
        }
        else activeBoostSpeed = 1;
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed,Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed,Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * activeBoostSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }
}
