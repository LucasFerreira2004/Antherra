using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Movement")]
public class NewMonoBehaviourScript : ScriptableObject, IVampireMovement
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minAttackDistance = 2f;
    [SerializeField] private float idealAttackDistance = 4f;

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
            // Foge
            direction = -toPlayer.normalized;
        }
        else
        {
            // Se aproxima
            direction = toPlayer.normalized;
        }

        context.MoveDirection = direction;
        context.LastDirection = direction != Vector2.zero ? direction : context.LastDirection;

        Vector2 newPos = context.Rigidbody.position + direction * moveSpeed * Time.deltaTime;
        context.Rigidbody.MovePosition(newPos);

        Animate(context);
    }

    private void Animate(VampireScript context)
    {
        context.Animator.SetFloat("MoveX", context.LastDirection.x);
        context.Animator.SetFloat("MoveY", context.LastDirection.y);
        context.Animator.SetFloat("Speed", context.MoveDirection.magnitude);
    }
}
