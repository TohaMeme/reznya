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


   

   
}
