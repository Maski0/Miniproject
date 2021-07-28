using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ModifyHealth(-10);
        }
    }
}
