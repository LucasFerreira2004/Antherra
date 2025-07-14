using UnityEngine;

public class OrcAttack : MonoBehaviour
{
    public float damage = 1f;

    private Animator animator;
    private OrcMovement movement;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<OrcMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking || !collision.CompareTag("Player"))
            return;

        isAttacking = true;
        movement.SetCanMove(false);

        animator?.SetTrigger("Attack");

        PlayerStatus playerStatus = collision.GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.TakeDamage(Mathf.RoundToInt(damage));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
            movement.SetCanMove(true);
        }
    }
}
