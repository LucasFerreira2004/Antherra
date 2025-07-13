using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerHealthIncresed;

    [SerializeField] private float speed = 8;
    [SerializeField] private int maxHealth = 8;
    [SerializeField] private int currentHealth = 8;
    [SerializeField] private float bulletDamage = 2;
    [SerializeField] private float bulletFireRate = 2;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float bulletRange = 1;
    public void Start()
    {
        speed = 8;
        maxHealth = 8;
        currentHealth = 8;
        bulletDamage = 2;
        bulletFireRate = 2;
        bulletSpeed = 10;
        bulletRange = 1;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        OnPlayerDamaged?.Invoke();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        int totalHealthAfterHeal = currentHealth + amount;
        if (totalHealthAfterHeal > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += amount;
    }

    private void Die()
    {
        Debug.Log("O jogador morreu.");
        // eventos, animações, etc.
    }

    //getters e setters
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value); // nunca negativo
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); // mínimo 1
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float BulletDamage
    {
        get => bulletDamage;
        set => bulletDamage = Mathf.Max(0, value);
    }

    public float FireRate
    {
        get => bulletFireRate;
        set => bulletFireRate = Mathf.Max(0.1f, value); // mínimo sensato para evitar divisão por 0
    }

    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = Mathf.Max(0.1f, value);
    }
    public float BulletRange
    {
        get => bulletRange;
        set => bulletRange = Mathf.Max(0.1f, value);
    }
}
