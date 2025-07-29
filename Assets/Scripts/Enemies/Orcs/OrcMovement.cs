using UnityEngine;

[CreateAssetMenu(menuName = "Orc/Movement")]
public class OrcMovement : ScriptableObject, IOrcMovement
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Vector2 smoothVelocity;
    [SerializeField] private Vector2 smoothedDirection;
    [SerializeField] private float obstacleCheckDistance = 0.5f;

    public float GetDistanceRadius()
    {
        return this.detectionRadius;
    }

    public void Move(OrcScript context)
    {
        if (context.State != OrcState.Moving) return;

        Vector2 desiredDirection = (context.Player.position - context.transform.position).normalized;

        if (IsPathBlocked(context, desiredDirection))
        {
            desiredDirection = Vector2.zero; // para completamente
        }

        context.MoveDirection = desiredDirection;
        context.LastDirection = desiredDirection;

        ApplyMovement(context);
        Animate(context);
    }

    private void ApplyMovement(OrcScript context)
    {
        Vector2 newPos = context.Rigidbody.position + context.MoveDirection * moveSpeed * Time.deltaTime;
        context.Rigidbody.MovePosition(newPos);
    }

    private bool IsPathBlocked(OrcScript context, Vector2 direction)
    {
        Vector2 origin = (Vector2)context.transform.position;
        float radius = 0.3f;

        RaycastHit2D hit = Physics2D.CircleCast(
            origin,
            radius,
            direction,
            obstacleCheckDistance,
            LayerMask.GetMask("Wall") // <- Filtro aplicado aqui!
        );

        Debug.DrawRay(origin, direction * obstacleCheckDistance, Color.yellow);

        return hit.collider != null;
    }

    private void Animate(OrcScript context)
    {
        Vector2 target = context.MoveDirection != Vector2.zero ? context.MoveDirection : context.LastDirection;
        smoothedDirection = Vector2.SmoothDamp(
            smoothedDirection,
            target,
            ref smoothVelocity,
            smoothTime
        );

        context.Animator.SetFloat("MoveX", smoothedDirection.x);
        context.Animator.SetFloat("MoveY", smoothedDirection.y);
        context.Animator.SetFloat("Speed", context.MoveDirection.magnitude);
    }
}
