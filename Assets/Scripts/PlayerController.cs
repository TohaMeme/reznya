using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float _forwardForce = 1f;
    [SerializeField] float _sideForce = 1f;

    [Header("Camera Settings")]
    [SerializeField] float _sensitivity = 200f;

 
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    void FixedUpdate()
    {
        float xMouseMovement = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var force = Vector3.zero;

        force += transform.forward * vertical * Time.fixedDeltaTime * _forwardForce;
        force += transform.right * horizontal * Time.fixedDeltaTime * _sideForce;


        float rotation = xMouseMovement * Time.fixedDeltaTime * _sensitivity;

        rb.AddForce(force);
        transform.Rotate(0, rotation, 0);
    }

}
