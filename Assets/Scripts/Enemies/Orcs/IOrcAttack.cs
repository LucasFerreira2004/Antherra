using UnityEngine;

public interface IOrcAttack
{
    void ApplyDamage();
    void HandleTriggerEnter(OrcScript context, Collider2D collision);

    void SetLastTarget(PlayerHealthStatus target);
}
