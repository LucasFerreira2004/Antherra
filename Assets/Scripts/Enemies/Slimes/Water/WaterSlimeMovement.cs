using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Movement/WaterSlime")]
public class WaterSlimeMovement : SlimeMovementStrategy
{
    public float switchTime = 2f;
    private float timer = 0f;
    private Vector2 direction = Vector2.right;

    [SerializeField] private float obstacleCheckDistance = 0.5f;

    public override void Move(SlimeScript context)
    {
        if (context.State != SlimeState.Moving) return;

        timer += Time.deltaTime;

        // Checa se há parede na frente — inverte imediatamente
        if (IsPathBlocked(context, direction))
        {
            direction *= -1;
            timer = 0f; // reinicia o timer ao bater na parede
        }

        // Move o slime
        context.Transform.Translate(direction * context.MoveSpeed * Time.deltaTime);

        // Alterna a direção com base no tempo (se não bater na parede)
        if (timer >= switchTime)
        {
            direction *= -1;
            timer = 0f;
        }

        // Atualiza animação
        if (context.Animator)
        {
            context.Animator.SetFloat("MoveX", direction.x);
            context.Animator.SetFloat("MoveY", direction.y);
        }
    }

    private bool IsPathBlocked(SlimeScript context, Vector2 direction)
    {
        Vector2 origin = (Vector2)context.Transform.position;
        float radius = 0.3f;

        RaycastHit2D hit = Physics2D.CircleCast(
            origin,
            radius,
            direction,
            obstacleCheckDistance,
            LayerMask.GetMask("Wall")
        );

        Debug.DrawRay(origin, direction * obstacleCheckDistance, Color.yellow);

        return hit.collider != null;
    }
}
