using UnityEngine;
using UnityEngine.AI;

public enum VampireState
{
    Attacking,
    Waiting,
    Moving,
}

public class VampireScript : MonoBehaviour, IEnemyTakeDamage
{
    public event System.Action<GameObject> OnEnemyDeath;

    [Header("Strategies")]
    [SerializeField] private ScriptableObject movementSO;
    [SerializeField] private ScriptableObject attackSO;
    private IVampireMovement movementStrategy => movementSO as IVampireMovement;
    private IVampireAttack attackStrategy => attackSO as IVampireAttack;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D rigidBody;

    public Rigidbody2D Rigidbody => rigidBody;
    public Animator Animator => animator;
    public Transform Player => player;
    public GameObject FireballPrefab => fireballPrefab;
    public Transform FirePoint => firePoint;

    [Header("Attributes")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float attackCooldown = 2f;

    public Vector2 MoveDirection;
    public Vector2 LastDirection = Vector2.down;
    public bool IsBlocked { get; set; }

    private float currentHealth;
    private float attackTimer = 0f;

    public VampireState State { get; private set; } = VampireState.Waiting;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        bool isOutOfIdealRange = distance >= movementStrategy.GetIdealDistance();
        bool isTooClose = distance <= movementStrategy.GetMinAttackDistance();
        bool shouldMove = isOutOfIdealRange || isTooClose;

        if (shouldMove && !IsBlocked)
        {
            SetState(VampireState.Moving);
        }
        else
        {
            SetState(VampireState.Attacking);
        }

        switch (State)
        {
            case VampireState.Waiting:
                Stop();
                break;

            case VampireState.Moving:
                movementStrategy?.Move(this);
                break;

            case VampireState.Attacking:
                Stop();
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldown)
                {
                    animator?.SetTrigger("Attack");
                    attackTimer = 0f;
                }
                break;
        }
    }

    public void Shoot()
    {
        attackStrategy?.Attack(this);
    }

    public void SetState(VampireState newState)
    {
        if (State == newState) return;
        State = newState;
    }


    private void Stop()
    {
        MoveDirection = Vector2.zero;

        // Atualiza para olhar na direção do player
        Vector2 lookDirection = player.position - transform.position;
        if (lookDirection.magnitude > 0.1f)
        {
            LastDirection = lookDirection.normalized;
        }

        animator.SetFloat("MoveX", LastDirection.x);
        animator.SetFloat("MoveY", LastDirection.y);
        animator.SetFloat("Speed", 0f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            animator?.SetTrigger("Death");
            return;
        }
        animator?.SetTrigger("Hurt");
    }

    public void Die()
    {
        OnEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
