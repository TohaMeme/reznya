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

    private void Awake()
    {
        characterController= GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    float verticalSpeed;

    private void Update()
    {
        float xMouseMovement = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        moveDirection = transform.TransformDirection(moveDirection) * _speed;
        moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal);

        Debug.DrawLine(transform.position, transform.position + moveDirection * 2, Color.blue);

        transform.Rotate(new Vector3(0, xMouseMovement * _sensitivity * Time.deltaTime, 0));

        if (characterController.isGrounded)
            verticalSpeed = 0;
        else
            verticalSpeed = -9.8f * Time.deltaTime;

        characterController.Move((moveDirection * _speed + Vector3.up * verticalSpeed ) * Time.deltaTime);
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);
        surfaceNormal = hit.normal;
    }




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
