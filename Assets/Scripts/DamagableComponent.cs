using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableComponent : MonoBehaviour
{
    [SerializeField] int maxHp = 100;
    [SerializeField] int minHp = 0;

    int currentHp = 100;

    bool isDead;

    private void Start()
    {
        currentHp = maxHp;
    }

    public int Hp
    {
        get => currentHp;
        set
        {
            if (isDead)
                return;

            currentHp = value;
            if(currentHp <= minHp)
            {
                Die();
            }
        }
    }

    public bool IsDead => isDead; //property
    public bool IsAlive => !isDead;

    public void DealDamage(int damageAmount)
    {
        currentHp -= damageAmount;
        if(currentHp <= 0)
        {
            Die();
        }
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void Heal(int healAmount)
    {
        if (isDead)
            return;

        currentHp += healAmount;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} is dead");
    }
}