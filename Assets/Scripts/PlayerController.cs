using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float _speed = 1f;
   // [SerializeField] float _forwardForce = 1f;
   // [SerializeField] float _sideForce = 1f;

    [Header("Camera Settings")]
    [SerializeField] float _sensitivity = 100f;

    CharacterController characterController;

    Vector3 surfaceNormal;

    float verticalSpeed;
    GameObject floor;
    GameObject Floor
    {
        get => floor;
        set
        {
            if (floor != value)
            {
                if (floor != null)
                    floor.SendMessage("OnCharacterExit", this, SendMessageOptions.DontRequireReceiver);
                if (value != null)
                    value.SendMessage("OnCharacterEnter", this, SendMessageOptions.DontRequireReceiver);
            }
                
            floor = value;
           
        }
    }


    private void Awake()
    {
        characterController= GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime);
        transform.Rotate(rotation);

        if (characterController.isGrounded)
            verticalSpeed = -0.1f;
        else
            verticalSpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);

        Vector3 velocity = transform.TransformDirection(input) * _speed;

        Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        Vector3 adjustedVelocity = slopeRotation * velocity;  

        velocity = adjustedVelocity.y < 0 ? adjustedVelocity : velocity;

        velocity.y += verticalSpeed;

        characterController.Move(velocity * Time.deltaTime);
        GroundCheck();
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);
        surfaceNormal = hit.normal;
    }

    void GroundCheck()
    {
        if(Physics.Linecast(transform.position, transform.position + Vector3.down * (characterController.height/2 + 0.5f), out RaycastHit hit))
        {
            Floor = hit.collider.gameObject;
            Floor.SendMessage("OnCharacterStay", this, SendMessageOptions.DontRequireReceiver);

        }
        else
        {
            Floor = null;
        }
    }

    /*private void Update()
    {
        float xMouseMovement = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal);

        Debug.DrawLine(transform.position, transform.position + moveDirection * 2, Color.blue);

        transform.Rotate(new Vector3(0, xMouseMovement * _sensitivity * Time.deltaTime, 0));

        if (characterController.isGrounded)
            verticalSpeed = 0;
        else
            verticalSpeed = -9.8f * Time.deltaTime;

        characterController.Move((moveDirection * _speed + Vector3.up * verticalSpeed ) * Time.deltaTime);
        
    }*/

    /*   void FixedUpdate()
       {
           float xMouseMovement = Input.GetAxis("Mouse X");
           float horizontal = Input.GetAxis("Horizontal");
           float vertical = Input.GetAxis("Vertical");

           var force = Vector3.zero;

           force += transform.forward * vertical * Time.fixedDeltaTime * _forwardForce;
           force += transform.right * horizontal * Time.fixedDeltaTime * _sideForce;


           float rotation = xMouseMovement * Time.fixedDeltaTime * _sensitivity;

           transform.Rotate(0, rotation, 0);
       } */

}
