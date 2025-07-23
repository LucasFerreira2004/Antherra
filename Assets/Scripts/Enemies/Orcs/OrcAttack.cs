using UnityEngine;

[CreateAssetMenu(menuName = "Orc/Attack")]
public class OrcAttack : ScriptableObject, IOrcAttack
{
    [Header("Configurações do ataque")]
    [SerializeField] private int damage = 1;

    private PlayerStatus lastTarget;

    public void ApplyDamage()
    {
        lastTarget?.TakeDamage(damage);
    }

    public void SetLastTarget(PlayerStatus target)
    {
        lastTarget = target;
    }

    public void HandleTriggerEnter(OrcScript context, Collider2D collision)
    {
        if (context.State != OrcState.Attacking) return;

        var target = collision.GetComponent<PlayerStatus>();

        lastTarget = target;
        context?.Animator.SetTrigger("Attack");
    }
}
