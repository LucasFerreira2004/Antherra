using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Attack/FireSlime")]
public class FireSlimeAttack : SlimeAttackStrategy
{

    public float bulletSpeed = 2f;
    public int bulletDamage = 1;
    public float bulletRange = 5f;

    public override void Attack(SlimeScript context)
    {
        if (context.Player == null) return;
        if (!context.State.Equals(SlimeState.Attacking)) return;

        GameObject fireball = GameObject.Instantiate(
            context.FireballPrefab,
            context.FirePoint.position,
            Quaternion.identity
        );

        // Inicializa a bala corretamente
        BulletScript bullet = fireball.GetComponent<BulletScript>();
        bullet?.Init(bulletRange, bulletDamage, context.gameObject);

        // Define a direção e velocidade
        Vector2 dir = (context.Player.position - context.FirePoint.position).normalized;
        fireball.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletSpeed;

        context.SetState(SlimeState.Moving);
    }
}
