using UnityEngine;

[CreateAssetMenu(menuName = "Slime/Attack/WaterSlime")]
public class WaterSlimeAttack : SlimeAttackStrategy
{
    public float attackCooldown = 3f;
    public float bulletSpeed = 2f;
    public float bulletDamage = 1f;
    public float bulletRange = 4f;

    public override void Attack(SlimeScript context)
    {
        if (context.State != SlimeState.Attacking) return;

        Vector2[] directions = new Vector2[]
        {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right,
                new Vector2(1, 1).normalized,
                new Vector2(-1, 1).normalized,
                new Vector2(1, -1).normalized,
                new Vector2(-1, -1).normalized
        };

        foreach (Vector2 dir in directions)
        {
            GameObject bullet = GameObject.Instantiate(
                context.FireballPrefab,
                context.FirePoint.position,
                Quaternion.identity
            );

            var bulletScript = bullet.GetComponent<BulletScript>();
            if (bulletScript != null)
                bulletScript.Init(bulletRange, bulletDamage, context.gameObject);

            var rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir * bulletSpeed;

            bullet.transform.right = dir;
        }

        context.SetState(SlimeState.Moving);
    }
}
