using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class OrcMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRadius = 5f;
    public float smoothTime = 0.1f;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveDirection;
    private Vector2 lastDirection = Vector2.down;
    private Vector2 smoothedDirection;
    private Vector2 smoothVelocity;

    private bool canMove = true;

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        MoveOrc();
        Animate();
    }

    private void MoveOrc()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius && canMove)
        {
            moveDirection = (player.position - transform.position).normalized;
            lastDirection = moveDirection;
            ApplyMovement();
        }
        else
        {
            moveDirection = Vector2.zero;
        }
    }

    private void ApplyMovement()
    {
        Vector2 newPos = rb.position + moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    private void Animate()
    {
        Vector2 target = moveDirection != Vector2.zero ? moveDirection : lastDirection;
        smoothedDirection = Vector2.SmoothDamp(smoothedDirection, target, ref smoothVelocity, smoothTime);

        animator.SetFloat("MoveX", smoothedDirection.x);
        animator.SetFloat("MoveY", smoothedDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }
}
