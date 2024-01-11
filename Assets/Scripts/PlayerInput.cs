using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    private InputControls inputControls;
    private Rigidbody2D rb;

    [Header("Movement")]
    private float speed = 5f;
    private Vector2 movementVector;

    [Header("Jumping")]
    private bool canJump = false;
    private float jumpForce = 7f;
    private Vector2 jumpVector = Vector2.up;
    

    private void Awake()
    {
        jumpVector *= jumpForce;
        rb = GetComponent<Rigidbody2D>();
        inputControls = new InputControls();
    }
    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void FixedUpdate()
    {
        if (canJump && inputControls.Controller.Jump.ReadValue<float>() > 0)
        {
            Jump();
            canJump = false;
        }
        movementVector = inputControls.Controller.Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(movementVector.x * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }
    private void OnDisable()
    {
        inputControls.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }
}
