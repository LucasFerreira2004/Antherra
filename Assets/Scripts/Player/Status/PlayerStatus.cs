using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerStatus : MonoBehaviour
{
    private static int playModeIndex = 0;
    public static Transform PlayerTransform { get; set; }

    private IBaseStatusStrategy baseStatusStrategy;
    private SpriteRenderer spriteRenderer;

    private enum PlayerState { Normal, Invincible }
    private PlayerState currentState = PlayerState.Normal;
    private float invincibleTimer = 0f;
    private const float invincibleDuration = 2f;

    [Header("Player Mode")]
    [SerializeField] private List<PlayerMode> playerModes;
    [SerializeField] private BaseStatusFactory baseStatusFactory;

    [Header("Health Settings")]
    [SerializeField] private PlayerHealthData healthData;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerHealthIncreased;

    private void Awake()
    {
        PlayerTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[0]);
        spriteRenderer.color = baseStatusStrategy.CharacterSpriteColor;
    }

    private void Update()
    {
        if (currentState == PlayerState.Invincible)
        {
            invincibleTimer -= Time.deltaTime;

            // Pulsa a opacidade com um efeito senoidal
            float alpha = 0.5f + Mathf.Sin(Time.time * 20f) * 0.25f; // entre 0.25 e 0.75
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;

            if (invincibleTimer <= 0f)
            {
                currentState = PlayerState.Normal;
                Color resetColor = spriteRenderer.color;
                resetColor.a = 1f;
                spriteRenderer.color = resetColor;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentState == PlayerState.Invincible) return;

        healthData.CurrentHealth -= amount;
        OnPlayerDamaged?.Invoke();

        if (healthData.CurrentHealth <= 0)
        {
            healthData.CurrentHealth = 0;
            Die();
            return;
        }

        // Ativa invencibilidade por 2 segundos
        currentState = PlayerState.Invincible;
        invincibleTimer = invincibleDuration;
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
