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
        // if (isAttacking) return;

        // timer += Time.deltaTime;

        // if (timer >= attackCooldown)
        // {
        //     StartCoroutine(AttackSequence());
        //     timer = 0f;
        // }
    }

    public System.Collections.IEnumerator AttackSequence()
    {
        isAttacking = true;

        movement?.SetCanMove(false);
        yield return new WaitForSeconds(stopBeforeAttack);

        animator?.SetTrigger("Attack");

        yield return new WaitForSeconds(delayAfterAttack);

        movement?.SetCanMove(true);
        isAttacking = false;
    }
}
