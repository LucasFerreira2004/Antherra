using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Movement/WaterSlime")]
public class WaterSlimeMovement : SlimeMovementStrategy
{
    public float switchTime = 2f;
    private float timer = 0f;
    private Vector2 direction = Vector2.right;

    public override void Move(SlimeContext context)
    {
        if (context.State != SlimeState.Moving) return;

        timer += Time.deltaTime;
        context.Transform.Translate(direction * context.MoveSpeed * Time.deltaTime);

        if (timer >= switchTime)
        {
            timer = 0f;
            direction *= -1;
        }

        if (context.Animator)
        {
            context.Animator.SetFloat("MoveX", direction.x);
            context.Animator.SetFloat("MoveY", direction.y);
        }
    }
}
