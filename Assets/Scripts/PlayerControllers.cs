using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{

    [SerializeField]
    float forwardSpeed = 1;
    [SerializeField]
    float sideForce = 1;

    Rigidbody rb;

    private void Awake()
    {
    
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(horizontal * Time.fixedDeltaTime * forwardSpeed, 0, vertical * Time.fixedDeltaTime * sideForce));

    }


}
