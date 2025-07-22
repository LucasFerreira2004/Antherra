using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerStatus : MonoBehaviour
{
    private static int playModeIndex = 0;
    public static Transform PlayerTransform { get; set; }
    private IBaseStatusStrategy baseStatusStrategy;

    [Header("Player Mode")]
    [SerializeField] private List<PlayerMode> playerModes;
    [SerializeField] private BaseStatusFactory baseStatusFactory;

    [Header("Health Settings")]
    [SerializeField] private PlayerHealthData healthData;

    [Header("Modifiers")]
    [SerializeField] private PlayerModifiedStatus modifiedStatus;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerHealthIncreased;


    private void Awake()
    {
        PlayerTransform = transform;
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[0]);
        GetComponent<SpriteRenderer>().color = baseStatusStrategy.CharacterSpriteColor;
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
        get => baseStatusStrategy.Speed * modifiedStatus.Speed;
        set => baseStatusStrategy.Speed = Mathf.Max(0, value);
    }

    public int BulletDamage
    {
        get => baseStatusStrategy.BulletDamage + modifiedStatus.BulletDamage;
        set => baseStatusStrategy.BulletDamage = value;
    }

    public float FireRate
    {
        get => baseStatusStrategy.BulletFireRate + modifiedStatus.BulletFireRate;
        set => baseStatusStrategy.BulletFireRate = Mathf.Max(0.1f, value);
    }

    public float BulletSpeed
    {
        get => baseStatusStrategy.BulletSpeed * modifiedStatus.BulletSpeed;
        set => baseStatusStrategy.BulletSpeed = Mathf.Max(0.1f, value);
    }

    public float BulletRange
    {
        get => baseStatusStrategy.BulletRange + modifiedStatus.BulletRange;
        set => baseStatusStrategy.BulletRange = Mathf.Max(0.1f, value);
    }

    // Vida do jogador
    public void TakeDamage(int amount)
    {
        healthData.CurrentHealth -= amount;
        OnPlayerDamaged?.Invoke();

        if (healthData.CurrentHealth <= 0)
        {
            healthData.CurrentHealth = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        healthData.CurrentHealth = Mathf.Min(healthData.CurrentHealth + amount, healthData.CurrentMaxHealth);
        OnPlayerHealed?.Invoke();
    }

    public void IncreaseMaxHealth(int amount)
    {
        healthData.CurrentMaxHealth += amount;
        healthData.CurrentHealth = healthData.CurrentMaxHealth;
        OnPlayerHealthIncreased?.Invoke();
    }

    private void Die()
    {
        SceneLoader.LoadGameOver();
        OnPlayerDeath?.Invoke();
    }

    public int MaxHealth
    {
        get => healthData.CurrentMaxHealth;
        set => healthData.CurrentMaxHealth = value;
    }

    public int CurrentHealth
    {
        get => healthData.CurrentHealth;
        set => healthData.CurrentHealth = Mathf.Clamp(value, 0, healthData.CurrentMaxHealth);
    }

    public List<PlayerMode> PlayerMode => playerModes;
}
