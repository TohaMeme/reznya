using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] 
    Transform cashedCamera;
    void Start()
    {
        cashedCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(cashedCamera.position.x, 0, cashedCamera.position.z));
    }
}
