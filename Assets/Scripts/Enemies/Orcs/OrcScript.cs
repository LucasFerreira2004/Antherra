using UnityEngine;

public enum OrcState
{
    Attacking,
    Waiting,
    Moving,
}

public class OrcScript : MonoBehaviour, ITakeDamage
{
    [Header("Actions")]
    [SerializeField] private ScriptableObject movementSO;
    [SerializeField] private ScriptableObject attackSO;

    private IOrcMovement movement;
    private IOrcAttack attack;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform player;


    public Rigidbody2D Rigidbody => rigidBody;
    public Animator Animator => animator;
    public Transform Player => player;

    [Header("Attributes")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;

    public Vector2 MoveDirection;
    public Vector2 LastDirection = Vector2.down;

    public OrcState State { get; private set; } = OrcState.Waiting;

    private bool CanMove = true;

    private void Start()
    {
        movement = movementSO as IOrcMovement;
        attack = attackSO as IOrcAttack;
        player = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {

        if (State != OrcState.Attacking) ValidateRadiusAndSetState();

        if (State == OrcState.Moving)
        {
            movement?.Move(this);
        }
        else if (State == OrcState.Waiting)
        {
            Stop();
        }
    }


    public void SetState(OrcState newState)
    {
        State = newState;
    }

    private void ValidateRadiusAndSetState()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= movement.GetDistanceRadius() && CanMove)
        {
            SetState(OrcState.Moving);
            return;
        }

        SetState(OrcState.Waiting);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetState(OrcState.Attacking);
            Stop();
            attack?.HandleTriggerEnter(this, collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetState(OrcState.Moving);
            attack?.SetLastTarget(null);
        }
    }

    private void Stop()
    {
        MoveDirection = Vector2.zero;
        animator.SetFloat("Speed", MoveDirection.magnitude);
    }

    public void TakeDamage(float damage)
    {
        SetState(OrcState.Waiting);
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            animator?.SetTrigger("Death");
            CanMove = false;
            return;
        }

        animator?.SetTrigger("Hurt");
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ApplyDamage()
    {
        attack?.ApplyDamage();
    }
}
