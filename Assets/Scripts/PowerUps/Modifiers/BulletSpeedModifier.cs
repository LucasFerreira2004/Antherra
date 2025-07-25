using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Bullet Speed Modifier")]
public class BulletSpeedModifier : IPowerUpEffectSO
{
    [SerializeField] private float bulletSpeedMultiplier = 1.2f;

    public override void Apply(PlayerStatus player)
    {
        player.ModifiedStatus.BulletSpeed *= bulletSpeedMultiplier;
    }
}
