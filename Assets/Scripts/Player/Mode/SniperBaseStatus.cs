using UnityEngine;

[CreateAssetMenu(menuName = "playerModes/sniper")]
public class SniperBaseStatus : IBaseStatusStrategy
{
    [SerializeField] private float speed = 8;
    [SerializeField] private int bulletDamage = 2;
    [SerializeField] private float bulletFireRate = 1;
    [SerializeField] private float bulletSpeed = 20;
    [SerializeField] private float bulletRange = 10;
    [SerializeField] private Color characterSpriteColor = Color.white;

    public override float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value);
    }

    public  override int BulletDamage
    {
        get => bulletDamage;
        set => bulletDamage = Mathf.Max(0, value);
    }

    public  override float BulletFireRate
    {
        get => bulletFireRate;
        set => bulletFireRate = Mathf.Max(0.1f, value);
    }

    public  override float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = Mathf.Max(0.1f, value);
    }

    public  override float BulletRange
    {
        get => bulletRange;
        set => bulletRange = Mathf.Max(0.1f, value);
    }

    public  override Color CharacterSpriteColor
    {
        get => characterSpriteColor;
        set => characterSpriteColor = value;
    }

}
