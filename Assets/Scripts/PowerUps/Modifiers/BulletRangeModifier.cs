using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Bullet Range Modifier")]
public class BulletRangeModifier : IPowerUpEffectSO
{
    [SerializeField] private float rangeIncrease = 2f;

      public override void Apply(PlayerStatus player)
    {
        player.ModifiedStatus.BulletRange += rangeIncrease;
    }
}

