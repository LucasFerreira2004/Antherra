using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Movement")]
public class NewMonoBehaviourScript : ScriptableObject, IVampireMovement
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minAttackDistance = 2f;
    [SerializeField] private float idealAttackDistance = 4f;

    private Vector2? alternateFleeDirection = null; // Guarda direção alternativa de fuga

    public float GetIdealDistance() => idealAttackDistance;

    public float GetMinAttackDistance() => minAttackDistance;

    public void Move(VampireScript context)
    {
        if (context.Player == null) return;

        Vector2 toPlayer = context.Player.position - context.transform.position;
        float distance = toPlayer.magnitude;
        Vector2 direction;

        if (distance < minAttackDistance)
        {
            // Está perto demais, tenta fugir

            if (alternateFleeDirection == null)
            {
                // Testa fuga direta
                Vector2 fleeDir = -toPlayer.normalized;
                RaycastHit2D hit = Physics2D.Raycast(context.transform.position, fleeDir, 0.5f, LayerMask.GetMask("Wall"));

                if (hit.collider != null)
                {
                    // Parede bloqueando, calcula direção alternativa

                    Vector2 around = (Vector2)context.Player.position + toPlayer.normalized * 2f;
                    Vector2 altDir = (around - (Vector2)context.transform.position).normalized;

                    // Testa se direção alternativa está livre
                    hit = Physics2D.Raycast(context.transform.position, altDir, 0.5f, LayerMask.GetMask("Wall"));
                    if (hit.collider == null)
                    {
                        alternateFleeDirection = altDir;
                        direction = altDir;
                    }
                    else
                    {
                        // Se as duas direções bloqueadas, apenas tenta fugir direto (vai bater na parede)
                        direction = fleeDir;
                    }
                }
                else
                {
                    // Fuga direta livre
                    direction = fleeDir;
                }
            }
            else
            {
                // Já temos direção alternativa, testa se ainda está livre
                RaycastHit2D hit = Physics2D.Raycast(context.transform.position, alternateFleeDirection.Value, 0.5f, LayerMask.GetMask("Wall"));
                if (hit.collider == null)
                {
                    direction = alternateFleeDirection.Value;
                }
                else
                {
                    // Direção alternativa bloqueada, resetar para recalcular
                    alternateFleeDirection = null;
                    direction = -toPlayer.normalized;
                }
            }
        }
        else
        {
            // Não está perto demais, zera direção alternativa e vai em direção ao player
            alternateFleeDirection = null;
            direction = toPlayer.normalized;
        }

        Vector2 nextPosition = context.Rigidbody.position + direction * moveSpeed * Time.deltaTime;

        context.MoveDirection = direction;
        context.LastDirection = direction != Vector2.zero ? direction : context.LastDirection;
        context.Rigidbody.MovePosition(nextPosition);

        Animate(context);
    }

    private void Animate(VampireScript context)
    {
        context.Animator.SetFloat("MoveX", context.LastDirection.x);
        context.Animator.SetFloat("MoveY", context.LastDirection.y);
        context.Animator.SetFloat("Speed", context.MoveDirection.magnitude);
    }
}
