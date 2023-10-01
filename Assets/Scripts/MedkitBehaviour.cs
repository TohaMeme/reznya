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
            other.GetComponent<DamagableComponent>().Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
