using UnityEngine;

[CreateAssetMenu(menuName = "Orc/Attack")]
public class OrcAttack : ScriptableObject, IOrcAttack
{
    [Header("Configurações do ataque")]
    [SerializeField] private float damage = 1f;

    public void Attack(OrcScript context)
    {
        // Poderia conter lógica automática, mas hoje o ataque é controlado via trigger
    }

    public void HandleTriggerEnter(OrcScript context, Collider2D collision)
    {
        if (context.State != OrcState.Attacking) return;

        context.Animator?.SetTrigger("Attack");

        var target = collision.GetComponent<PlayerStatus>();
        if (target != null)
        {
            // target.TakeDamage(damage); // Supondo que você tenha esse método
        }
    }
}
