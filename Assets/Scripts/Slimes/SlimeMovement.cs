using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.right;
    public float switchDirectionTime = 2f;

    private Animator animator;
    private float timer;
    private bool canMove = true;

    private Vector2 lastDirection = Vector2.right; // nova variável

    public void SetCanMove(bool value) => canMove = value;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetFloat("MoveX", lastDirection.x);
            animator.SetFloat("MoveY", lastDirection.y);
            return;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Atualiza a última direção válida
        if (moveDirection != Vector2.zero)
        {
            lastDirection = moveDirection;
        }

        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);

        timer += Time.deltaTime;
        if (timer >= switchDirectionTime)
        {
            moveDirection *= -1;
            timer = 0f;
        }
    }
}
