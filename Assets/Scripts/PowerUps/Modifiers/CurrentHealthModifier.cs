using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Current Health Modifier")]
public class CurrentHealthModifier : IPowerUpEffectSO
{
    [SerializeField] private int HealthIncrese = 1;

    public override void Apply(PlayerStatus player)
    {
        player.Heal(HealthIncrese);
    }
}
