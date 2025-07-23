using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Attack/DeathSlime/Shoot")]
public class DeathSlimeShootAttack : SlimeAttackStrategy
{
    public float bulletSpeed = 2f;
    public int bulletDamage = 1;
    public float bulletRange = 5f;
    public float spreadAngle = 15f;
    public float centralBulletSpeedMultiplier = 1.2f; // Aumenta a velocidade do projétil central

    public override void Attack(SlimeScript context)
    {
        if (context.Player == null) return;
        if (!context.State.Equals(SlimeState.Attacking)) return;

        Vector2 dir = (context.Player.position - context.FirePoint.position).normalized;

        // Projétil central com velocidade aumentada
        FireBullet(context, dir, bulletSpeed * centralBulletSpeedMultiplier);

        // Projéteis laterais com velocidade normal
        FireBullet(context, RotateVector(dir, spreadAngle), bulletSpeed);
        FireBullet(context, RotateVector(dir, -spreadAngle), bulletSpeed);

        context.SetState(SlimeState.Moving);
    }

    private void FireBullet(SlimeScript context, Vector2 direction, float speed)
    {
        GameObject fireball = GameObject.Instantiate(
            context.FireballPrefab,
            context.FirePoint.position,
            Quaternion.identity
        );

        BulletScript bullet = fireball.GetComponent<BulletScript>();
        bullet?.Init(bulletRange, bulletDamage, context.gameObject);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }

        // Rotaciona o sprite da bala para apontar na direção
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        ).normalized;
    }
}
