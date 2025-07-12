using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletFireRate = 2;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float bulletRange = 1;
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
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
