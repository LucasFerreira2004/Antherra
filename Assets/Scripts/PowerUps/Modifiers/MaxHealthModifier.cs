using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Max Health Modifier")]
public class MaxHealthModifier : IPowerUpEffectSO
{
    [SerializeField] private int MaxHealthIncrese = 1;

    public override void Apply(PlayerStatus player)
    {
        player.IncreaseMaxHealth(MaxHealthIncrese);
    }
}
