using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    [SerializeField] int damageAmount;
    DamagableComponent _damagableComponent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _damagableComponent = other.GetComponent<DamagableComponent>();
            InvokeRepeating("InLava", 0.2f, 1f);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            CancelInvoke("InLava");
        }
            
    }

    void InLava()
    {
        _damagableComponent.DealDamage(damageAmount);
    }
}
