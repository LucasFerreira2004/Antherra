using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private PlayerStatus status;
    private Vector2 movementInputValue;
    private Vector2 movementInputSmoothVelocity;
    private Vector2 movementInputSmooth;

    [SerializeField] private Animator animator;
    [SerializeField] private float smoothnessTime = 0.05f;

    void Start()
    {
        status = GetComponent<PlayerStatus>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Animate();
    }

    void FixedUpdate()
    {
        movementInputSmooth = Vector2.SmoothDamp(
            movementInputSmooth,
            movementInputValue,
            ref movementInputSmoothVelocity,
            smoothnessTime
        );
        rigidbody2D.linearVelocity = movementInputSmooth * status.Speed;
    }

    void OnMove(InputValue input)
    {
        movementInputValue = input.Get<Vector2>();
        movementInputValue.Normalize();

        Debug.Log("Input recebido: " + movementInputValue);
    }

    private void Animate()
    {
        bool isMoving = movementInputValue.magnitude > 0.1f;

        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("moveX", movementInputValue.x);
            animator.SetFloat("moveY", movementInputValue.y);
        }
    }
}
