using UnityEngine;

[CreateAssetMenu(menuName = "playerModes/standart")]
public class StandardBaseStatus : BaseStatusStrategy
{

    [SerializeField] private float speed = 8;
    [SerializeField] private int maxHealth = 8;
    [SerializeField] private int currentHealth = 8;
    [SerializeField] private float bulletDamage = 2;
    [SerializeField] private float bulletFireRate = 2;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float bulletRange = 1;

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value);
    }

    public override int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value);
    }

    public override int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public override float BulletDamage
    {
        get => bulletDamage;
        set => bulletDamage = Mathf.Max(0, value);
    }

    public override float BulletFireRate
    {
        get => bulletFireRate;
        set => bulletFireRate = Mathf.Max(0.1f, value);
    }

    public override float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = Mathf.Max(0.1f, value);
    }

    public override float BulletRange
    {
        get => bulletRange;
        set => bulletRange = Mathf.Max(0.1f, value);
    }
}
