using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maximumHealth;
    [SerializeField] protected int health;

    public event Action<int> OnHealthLost;
    public event Action<int> OnHealthGained;
    public event Action OnHealthZero;

    public void LoseHealth(int damage)
	{
        health = Mathf.Clamp(health - damage, 0, maximumHealth);
        OnHealthLost?.Invoke(damage);

        if (health <= 0)
        {
            OnHealthZero?.Invoke();
        }
    }

    public void GainHealth(int healed)
	{
        health = Mathf.Clamp(health + healed, 0, maximumHealth);
        OnHealthGained?.Invoke(healed);

        if(health <= 0)
		{
            OnHealthZero?.Invoke();
		}
        
	}
}
