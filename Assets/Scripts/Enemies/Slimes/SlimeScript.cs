using UnityEngine;

public enum SlimeState
{
    Moving,
    Attacking,
    Waiting
}

public class SlimeScript : MonoBehaviour, IEnemyTakeDamage
{
    public event System.Action<GameObject> OnEnemyDeath;

    [Header("Strategies")]
    public SlimeMovementStrategy movementStrategy;
    public SlimeAttackStrategy attackStrategy;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform selfTransform;
    public Transform Transform => selfTransform;
    public Animator Animator => animator;
    public Transform Player => player;
    public float MoveSpeed => moveSpeed;
    public GameObject FireballPrefab => fireballPrefab;
    public Transform FirePoint => firePoint;
    public bool IsBlocked = false;

    [Header("Attributes")]
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;


    protected float timer = 0f;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        selfTransform = transform;
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        movementStrategy?.Move(this);
        attackStrategy?.Attack(this);

        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            timer = 0f;
            animator?.SetTrigger("Attack");
        }
    }

    public SlimeState State { get; private set; } = SlimeState.Moving;

    public void SetState(SlimeState newState)
    {
        State = newState;
    }

    public void TakeDamage(float damage)
    {
        SetState(SlimeState.Waiting);
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            animator?.SetTrigger("Death");
            return;
        }

        animator?.SetTrigger("Hurt");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsBlocked = true;
    }

    private void OnOnCollisionExit2D(Collision2D collision)
    {
        IsBlocked = false;
    }

    public void Die()
    {
        OnEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
