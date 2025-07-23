using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Multi Attribute Modifier")]
public class MultiAttributeModifier : IPowerUpEffectSO
{
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private int bulletDamageIncrease = 0;
    [SerializeField] private float fireRateMultiplier = 1f;
    [SerializeField] private float bulletSpeedMultiplier = 1f;
    [SerializeField] private float bulletRangeIncrease = 0f;
    [SerializeField] private int MaxHealthIncrese = 0;
    [SerializeField] private int HealthIncrese = 0;


    public override void Apply(PlayerStatus player)

    {
        player.ModifiedStatus.Speed *= speedMultiplier;
        player.ModifiedStatus.BulletDamage += bulletDamageIncrease;
        player.ModifiedStatus.BulletFireRate *= fireRateMultiplier;
        player.ModifiedStatus.BulletSpeed *= bulletSpeedMultiplier;
        player.ModifiedStatus.BulletRange += bulletRangeIncrease;
        player.IncreaseMaxHealth(MaxHealthIncrese);
        player.Heal(HealthIncrese);
    }
}
