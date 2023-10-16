using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] bool goesUp = true;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.transform.SetParent(gameObject.transform);

            Invoke("ManageAnimation", delay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            gameObject.transform.DetachChildren();
        }
    }
    void ManageAnimation()
    {
        if (goesUp)
        {
            GetComponent<Animator>().Play("GoUp");
            goesUp = false;
        }
        else
        {
            GetComponent<Animator>().Play("GoDown");
            goesUp = true;
        }
    }    
}
