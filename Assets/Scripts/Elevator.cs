using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : FloorType
{
    [SerializeField] float delay = 1f;
    [SerializeField] bool goesUp = true;
    [SerializeField] Vector3 destinationDown;
    [SerializeField] Vector3 destinationUp;
    [SerializeField] float initialDuration = 1f;
    Vector3 newDestination;
    

    public override void OnCharacterEnter(DamagableComponent damagableComponent)
    {
        PlayerController controller = damagableComponent.GetComponent<PlayerController>();
        controller.transform.SetParent(this.transform);
        GetTargetDestination();
      
        StartCoroutine(AnimateElevator(newDestination));
    }

    public override void OnCharacterExit(DamagableComponent damagableComponent)
    {
        StopAllCoroutines();

        if (goesUp) 
            goesUp = false;
        else
            goesUp = !goesUp;
        this.transform.DetachChildren();

    }

    IEnumerator AnimateElevator(Vector3 destination)
    {

        yield return new WaitForSeconds(delay);
        Vector3 start = transform.position;
        float time = 0;
        while (time < initialDuration)
        {
            Debug.Log("Coroutine");
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, destination, time);
            yield return null;
        }
        transform.position = destination;
       
    }

    void GetTargetDestination()
    {

        if (goesUp)
        {
            newDestination = destinationUp;
        }
        else
        {
            newDestination = destinationDown;
        }
    }

    /*
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
    */
}
