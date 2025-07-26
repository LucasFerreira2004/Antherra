using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Attack")]
public class VampireAttack : ScriptableObject, IVampireAttack
{
    [Header("Configuração de ataque")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private int damage = 1;

    public void Attack(VampireScript context)
    {
        if (context.Player == null || context.FirePoint == null || context.State != VampireState.Attacking) return;

        Vector2 direction = (context.Player.position - context.FirePoint.position).normalized;

        GameObject fireball = GameObject.Instantiate(
            context.FireballPrefab,
            context.FirePoint.position,
            Quaternion.identity
        );

        // Inicializa a bala corretamente
        BulletScript bullet = fireball.GetComponent<BulletScript>();
        bullet?.Init(100f, damage, context.gameObject);

        // Define a direção e velocidade
        Vector2 dir = (context.Player.position - context.FirePoint.position).normalized;
        fireball.GetComponent<Rigidbody2D>().linearVelocity = dir * projectileSpeed;

        // Rotaciona visual
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
