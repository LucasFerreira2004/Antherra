using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    private static int playModeIndex = 0;
    private BaseStatusStrategy baseStatusStrategy;

    [Header("Player Mode")]
    [SerializeField] private List<PlayerMode> playerModes;
    [SerializeField] private BaseStatusFactory baseStatusFactory;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerHealthIncreased;

    private void Awake()
    {
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[0]);
        GetComponent<SpriteRenderer>().color = baseStatusStrategy.CharacterSpriteColor;

        currentHealth = maxHealth;
    }

    public void OnChangeMode()
    {
        playModeIndex = (playModeIndex + 1) % playerModes.Count;
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[playModeIndex]);
        GetComponent<SpriteRenderer>().color = baseStatusStrategy.CharacterSpriteColor;
    }

    // Delegações de status base
    public float Speed
    {
        get => baseStatusStrategy.Speed;
        set => baseStatusStrategy.Speed = Mathf.Max(0, value);
    }

    public int BulletDamage
    {
        get => baseStatusStrategy.BulletDamage;
        set => baseStatusStrategy.BulletDamage = value;
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

    // Vida do jogador
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

    public List<PlayerMode> PlayerMode => playerModes;
}
