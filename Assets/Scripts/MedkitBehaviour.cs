using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitBehaviour : MonoBehaviour
{
    [SerializeField] int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            if (other.GetComponent<DamagableComponent>().Hp != 100)
            {
                other.GetComponent<DamagableComponent>().Hp += healAmount;
                Destroy(this.gameObject);
            }
            
            
        }
    }
}
