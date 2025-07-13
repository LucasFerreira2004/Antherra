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
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (fireCountinuosly || fireSingle)
        {
            if (Time.time >= nextFireTime && shootDirection != Vector2.zero)
            {
                FireBullet();
                nextFireTime = Time.time + 1f / status.FireRate;  // ex: 1/2 = 2 shoots per second
                fireSingle = false;
            }
            
        }
    }

    void OnAttackDirection(InputValue inputValue)
    {
        Vector2 inputDir = inputValue.Get<Vector2>();
        Debug.Log("direção do input de tiro" + inputDir);
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

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * status.BulletSpeed;
        bullet.GetComponent<BulletScript>().Init(status.BulletRange, status.BulletDamage);
    }
}
