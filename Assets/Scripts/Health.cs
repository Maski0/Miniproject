using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;
    [SerializeField]
    private int getDamage = 20;

    private int CurrentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;

        float currentHealthPct = (float)CurrentHealth / (float)MaxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("playerbullet"))
        {
            ModifyHealth(-getDamage);
        }
    }
}
