using System;
using UnityEngine;

public class PlayerHealthStatus : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerHealthIncreased;

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
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
