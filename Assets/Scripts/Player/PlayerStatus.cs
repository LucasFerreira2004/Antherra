using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerHealthIncresed;

    [SerializeField] private BaseStatusStrategy baseStatusStrategy;

    public void TakeDamage(int amount)
    {
        baseStatusStrategy.CurrentHealth -= amount;
        OnPlayerDamaged?.Invoke();
        if (baseStatusStrategy.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int totalHealthAfterHeal = baseStatusStrategy.CurrentHealth + amount;
        if (totalHealthAfterHeal > baseStatusStrategy.MaxHealth)
            baseStatusStrategy.CurrentHealth = baseStatusStrategy.MaxHealth;
        else
            baseStatusStrategy.CurrentHealth += amount;

        OnPlayerHealed?.Invoke();
    }

    private void Die()
    {
        Debug.Log("O jogador morreu.");
        OnPlayerDeath?.Invoke();
    }

    // Getters e setters delegando ao baseStatusStrategy
    public float Speed
    {
        get => baseStatusStrategy.Speed;
        set => baseStatusStrategy.Speed = Mathf.Max(0, value);
    }

    public int MaxHealth
    {
        get => baseStatusStrategy.MaxHealth;
        set => baseStatusStrategy.MaxHealth = Mathf.Max(1, value);
    }

    public int CurrentHealth
    {
        get => baseStatusStrategy.CurrentHealth;
        set => baseStatusStrategy.CurrentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float BulletDamage
    {
        get => baseStatusStrategy.BulletDamage;
        set => baseStatusStrategy.BulletDamage = Mathf.Max(0, value);
    }

    public float FireRate
    {
        get => baseStatusStrategy.BulletFireRate;
        set => baseStatusStrategy.BulletFireRate = Mathf.Max(0.1f, value);
    }

    public float BulletSpeed
    {
        get => baseStatusStrategy.BulletSpeed;
        set => baseStatusStrategy.BulletSpeed = Mathf.Max(0.1f, value);
    }

    public float BulletRange
    {
        get => baseStatusStrategy.BulletRange;
        set => baseStatusStrategy.BulletRange = Mathf.Max(0.1f, value);
    }
}
