using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagableComponent : MonoBehaviour
{
    [SerializeField] int maxHp = 100;
    [SerializeField] Affiliation affiliation;

    public Affiliation Affiliation => affiliation;

    public TMP_Text healthText;

    [SerializeField] int currentHp = 100;

    bool isDead;

    private void Start()
    {
        currentHp = maxHp;
    }
    public void Update()
    {
        if (healthText != null) healthText.text = "Health: " + currentHp + "%";
    }

    public int Hp
    {
        get => currentHp;
        set
        {
            if (isDead)
                return;
            if (currentHp == 100)
                return;
            currentHp = value;
            if(currentHp <= 0)
            {
                Die();
            }
            if (currentHp > 100)
            {
                currentHp = maxHp;
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
        Debug.Log($"Current HP = {currentHp}");
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} is dead");
    }

}
