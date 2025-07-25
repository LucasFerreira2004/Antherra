using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private bool fireCountinuosly;
    private bool fireSingle;
    private PlayerStatus status;
    private Vector2 shootDirection = Vector2.right;
    private float nextFireTime = 0f;
    void Start()
    {
        status = status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (fireCountinuosly || fireSingle)
        {
            if (Time.time >= nextFireTime && shootDirection != Vector2.zero)
            {
                FireBullet(status.BulletNumber);
                Debug.Log("fireRate:" + status.FireRate);
                nextFireTime = Time.time + 1f / status.FireRate;  // ex: 1/2 = 2 shoots per second
                fireSingle = false;
            }

        }
    }

    void OnAttackDirection(InputValue inputValue)
    {
        Vector2 inputDir = inputValue.Get<Vector2>();

        if (inputDir != Vector2.zero)
        {
            shootDirection = inputDir.normalized;
        }
    }

    void OnAttack(InputValue inputValue)
    {
        fireCountinuosly = inputValue.isPressed;
        if (inputValue.isPressed)
        {
            fireSingle = true;
        }
    }

    void FireBullet(int bulletCount)
    {
        float spreadAngle = 30f; // ângulo total do cone de espalhamento
        Vector2 shootDir = shootDirection.normalized;

        if (bulletCount == 1)
        {
            // Tiro simples, mesma lógica original
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootDir * status.BulletSpeed;
            bullet.GetComponent<BulletScript>().Init(status.BulletRange, status.BulletDamage, gameObject);
        }
        else
        {
            float angleStep = spreadAngle / (bulletCount - 1);
            float startAngle = -spreadAngle / 2f;

            float baseAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = baseAngle + startAngle + angleStep * i;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = dir * status.BulletSpeed;
                bullet.GetComponent<BulletScript>().Init(status.BulletRange, status.BulletDamage, gameObject);
            }
        }
    }

}
