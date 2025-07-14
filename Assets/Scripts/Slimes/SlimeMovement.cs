using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.right;
    public float switchDirectionTime = 2f;

    private Animator animator;
    private float timer;
    private bool canMove = true;
    private Vector2 lastDirection = Vector2.right;

    private SlimeAttack slimeAttack;
    private bool isChangingDirection = false;

    public void SetCanMove(bool value) => canMove = value;

    void Start()
    {
        animator = GetComponent<Animator>();
        slimeAttack = GetComponent<SlimeAttack>();
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

        if (moveDirection != Vector2.zero)
        {
            lastDirection = moveDirection;
        }

        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);

        timer += Time.deltaTime;

        if (!isChangingDirection && timer >= switchDirectionTime)
        {
            timer = 0f;
            StartCoroutine(ChangeDirectionWithAttack());
        }
    }

    private System.Collections.IEnumerator ChangeDirectionWithAttack()
    {
        isChangingDirection = true;
        SetCanMove(false);

        if (slimeAttack != null)
            yield return slimeAttack.AttackSequence(); // Aguarda o ataque terminar

        moveDirection *= -1;

        SetCanMove(true);
        isChangingDirection = false;
    }
}
