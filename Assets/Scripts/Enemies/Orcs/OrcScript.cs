using UnityEngine;

public enum OrcState
{
    Attacking,
    Waiting,
    Moving,
}

public class OrcScript : MonoBehaviour
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

    public Vector2 MoveDirection;
    public Vector2 LastDirection = Vector2.down;

    public OrcState State { get; private set; } = OrcState.Waiting;

    private void Start()
    {
        movement = movementSO as IOrcMovement;
        attack = attackSO as IOrcAttack;
        player = GameObject.FindWithTag("Player").transform;
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

        if (distance <= movement.GetDistanceRadius())
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
            SetState(OrcState.Moving); // Voltará a andar se o player ainda estiver no raio
        }
    }

    private void Stop()
    {
        MoveDirection = Vector2.zero;
        animator.SetFloat("Speed", MoveDirection.magnitude);
    }
}
