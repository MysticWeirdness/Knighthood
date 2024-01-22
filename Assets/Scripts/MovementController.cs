using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement")]
    private float speed = 5f;
    private Vector2 movementVector;

    [Header("Offsets")]
    private float playerYOffset = 0.65f;
    private float firstRayOffset = 0.55f;
    private float secondRayOffset = 0.32f;
    private float thirdRayOffset = 0.05f;

    [Header("Jumping")]
    private bool canJump = false;
    private float jumpForce = 5f;
    private Vector2 jumpVector = Vector2.up;

    private void Awake()
    {
        jumpVector *= jumpForce;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void SetMovementVector(Vector2 movement)
    {
        movementVector = movement;
    }

    private void Move()
    {
        rb.velocity = new Vector2(movementVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
        canJump = false;
    }

    public bool Grounded()
    {
        if (Physics2D.Raycast(new Vector3(transform.position.x - firstRayOffset, transform.position.y - playerYOffset, 0f), Vector2.down * 0.2f).transform.CompareTag("Floor") || Physics2D.Raycast(new Vector3(transform.position.x - secondRayOffset, transform.position.y - playerYOffset, 0f), Vector2.down * 0.2f).transform.CompareTag("Floor") || Physics2D.Raycast(new Vector3(transform.position.x - thirdRayOffset, transform.position.y - playerYOffset, 0f), Vector2.down * 0.2f).transform.CompareTag("Floor"))
        {
            return true;
        }
        return false;
    }
}
