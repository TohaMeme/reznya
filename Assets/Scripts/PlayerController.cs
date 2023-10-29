using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : BaseCharacterController
{

    [Header("Camera Settings")]
    [SerializeField] float _sensitivity = 100f;


    protected override void Awake()
    { 
        base.Awake();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Rotate(Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime);
        MoveLocal(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
