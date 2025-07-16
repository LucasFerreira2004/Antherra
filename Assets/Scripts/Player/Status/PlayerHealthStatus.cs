using System;
using UnityEngine;

public class PlayerHealthStatus : MonoBehaviour
{
    public event Action OnPlayerDamaged;
    public event Action OnPlayerDeath;
    public event Action OnPlayerHealed;
    public event Action OnPlayerHealthIncreased;

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("STATUS --> currentHealth em playerHealthStatus: " + currentHealth);
        Debug.Log("OnPlayerDamege invocado");
        OnPlayerDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnPlayerHealed?.Invoke();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        OnPlayerHealthIncreased?.Invoke();
    }

    private void Die()
    {
        Debug.Log("O jogador morreu.");
        OnPlayerDeath?.Invoke();
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value; 
    }
    

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }
}
