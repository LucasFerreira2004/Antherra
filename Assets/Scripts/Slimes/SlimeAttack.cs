using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public float attackCooldown = 3f;
    public float stopBeforeAttack = 0.3f;
    public float delayAfterAttack = 0.5f;

    private float timer;
    private SlimeMovement movement;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        movement = GetComponent<SlimeMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAttacking) return;

        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            StartCoroutine(AttackSequence());
            timer = 0f;
        }
    }

    private System.Collections.IEnumerator AttackSequence()
    {
        isAttacking = true;

        // Para antes de atacar
        movement?.SetCanMove(false);
        yield return new WaitForSeconds(stopBeforeAttack);

        // Toca animação de ataque
        animator?.SetTrigger("Attack");

        // Espera o delay após o ataque antes de voltar a andar
        yield return new WaitForSeconds(delayAfterAttack);

        movement?.SetCanMove(true);
        isAttacking = false;
    }
}
