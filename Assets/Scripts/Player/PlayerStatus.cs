using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;

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

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Max(0, value);
    }

    public float FireRate
    {
        get => fireRate;
        set => fireRate = Mathf.Max(0.1f, value); // mínimo sensato para evitar divisão por 0
    }
}
