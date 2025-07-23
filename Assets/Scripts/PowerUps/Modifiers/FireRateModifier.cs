using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Fire Rate Modifier")]
public class FireRateModifier : IPowerUpEffectSO
{
    [SerializeField] private float fireRateMultiplier = 0.9f; // menor é mais rápido

    public override void Apply(PlayerStatus player)

    {
        player.ModifiedStatus.BulletFireRate *= fireRateMultiplier;
    }
}
