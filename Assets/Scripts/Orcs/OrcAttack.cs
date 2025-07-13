using UnityEngine;

public class OrcAttack : MonoBehaviour
{
    public float damage = 5f;

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

        // Aqui você pode aplicar o dano ao player se quiser
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
