using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStatus/Modifiers")]
public class PlayerModifiedStatus : IBaseStatus
{
    [SerializeField] private float speed = 1; //multiply
    [SerializeField] private int bulletDamage = 0; //sum
    [SerializeField] private float bulletFireRate = 0; //sum
    [SerializeField] private float bulletSpeed = 1; //multiply
    [SerializeField] private float bulletRange = 0; //sum

    public void ResetValues()
    {
        speed = 1;
        bulletDamage = 0;
        bulletFireRate = 0;
        bulletSpeed = 1;
        bulletRange = 0;
    }

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value);
    }

    public override int BulletDamage
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
