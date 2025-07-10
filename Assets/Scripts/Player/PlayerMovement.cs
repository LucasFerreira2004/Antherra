using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private PlayerStatus status;
    private Vector2 movementInputValue;
    private Vector2 movementInputSmoothVelocity;
    private Vector2 movementInputSmooth;


    [SerializeField]
    private float smoothnessTime = 0.05f;
    void Start()
    {
        status = this.gameObject.GetComponent<PlayerStatus>();
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
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
    }

    //getters and setters
}
