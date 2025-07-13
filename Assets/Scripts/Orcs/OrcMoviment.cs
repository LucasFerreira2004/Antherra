using System;
using UnityEngine;

public class OrcMovement : MonoBehaviour
{
    [Header("Orc Settings")]
    public float moveSpeed = 2f;
    public float damage = 5f;
    public float detectionRadius = 5f;
    public float stopDistance = 1.2f;
    [SerializeField] private float smoothTime = 0.1f; // tempo de suavização

    private Transform player;
    private Animator animator;
    private Vector2 moveDirection;
    private Vector2 lastDirection = Vector2.down;

    // Novas variáveis para suavização
    private Vector2 smoothedDirection;
    private Vector2 smoothVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj == null)
        {
            Debug.LogWarning("Player não encontrado! Verifique se o PlayerPrefab está com a tag 'Player'.");
            return;
        }

        player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (CanApplyMoviment(distanceToPlayer))
        {
            moveDirection = (player.position - transform.position).normalized;
            lastDirection = moveDirection;

            transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        Animate(); // ← sempre anima
    }

    void Animate()
    {
        if (animator == null) return;

        // Suaviza direção (apenas visual)
        Vector2 targetDirection = moveDirection != Vector2.zero ? moveDirection : lastDirection;
        smoothedDirection = Vector2.SmoothDamp(smoothedDirection, targetDirection, ref smoothVelocity, smoothTime);

        animator.SetFloat("MoveX", smoothedDirection.x);
        animator.SetFloat("MoveY", smoothedDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    private bool CanApplyMoviment(float distanceToPlayer)
    {
        return distanceToPlayer <= detectionRadius && distanceToPlayer > stopDistance;
    }
}
