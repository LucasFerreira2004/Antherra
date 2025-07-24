using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Bullet Damage Modifier")]
public class BulletDamageModifier : IPowerUpEffectSO
{
    [SerializeField] private int damageIncrease = 1;

    public override void Apply(PlayerStatus player)
    {
        player.ModifiedStatus.BulletDamage += damageIncrease;
    }
}
