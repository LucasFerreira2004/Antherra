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
    public const float invincibleDuration = 2f;

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

    // Delegações de status base
    public PlayerModifiedStatus ModifiedStatus
    {
        get => modifiedStatus;
    }
    public float Speed
    {
        get => baseStatusStrategy.Speed * modifiedStatus.Speed;
        set => baseStatusStrategy.Speed = Mathf.Max(0, value);
    }

    public int BulletDamage
    {
        get => baseStatusStrategy.BulletDamage + modifiedStatus.BulletDamage;
        set => modifiedStatus.BulletDamage = value;
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

    // tirar atributos de vida de playerStatus e deixar apenas em PlayerHealthData
    public void TakeDamage(int amount)
    {
        healthData.CurrentHealth -= amount;
        OnPlayerDamaged?.Invoke();

        if (healthData.CurrentHealth <= 0)
        {
            healthData.CurrentHealth = 0;
            Die();
        }

        // Ativa invencibilidade por 2 segundos
        currentState = PlayerState.Invincible;
        invincibleTimer = invincibleDuration;
    }

    public void Heal(int amount)
    {
        if (amount == 0) return;
        healthData.CurrentHealth = Mathf.Min(healthData.CurrentHealth + amount, healthData.CurrentMaxHealth);
        OnPlayerHealed?.Invoke();
    }

    public void IncreaseMaxHealth(int amount)
    {
        if (amount == 0) return;
        healthData.CurrentMaxHealth += amount;
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

    public void OnChangeMode()
    {
        playModeIndex = (playModeIndex + 1) % playerModes.Count;
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[playModeIndex]);
        GetComponent<SpriteRenderer>().color = baseStatusStrategy.CharacterSpriteColor;
    }

    public List<PlayerMode> PlayerMode => playerModes;
}
