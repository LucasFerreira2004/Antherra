using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Speed Modifier")]
public class SpeedModifier : IPowerUpEffectSO
{
    [SerializeField] private float speedMultiplier = 1.2f;

    public override void Apply(PlayerStatus playerStatus)
    {
        playerStatus.ModifiedStatus.Speed *= speedMultiplier;
    }
}
