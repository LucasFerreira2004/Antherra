using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Movement/FireSlime")]
public class FireSlimeMovement : SlimeMovementStrategy
{
    private Vector2 direction = Vector2.right;
    public override void Move(SlimeScript context)
    {
        Vector2 direction = (context.Player.position - context.Transform.position).normalized;

        context.Animator.SetFloat("MoveX", direction.x);
        context.Animator.SetFloat("MoveY", direction.y);
    }
}
