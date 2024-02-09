using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    private InputControls controls;
    private Rigidbody2D rb;

    private float movSpeed = 2f;
    private bool onLadder = false;
    private bool touchingLadder = false;
    [SerializeField] private bool isGrounded = true;
    private bool canJump = true;
    private float jumpForce = 5f; 
    private float ladderSpeed = 0.1f;

    private Vector3 boxSize;
    private float maxDistance;
    [SerializeField] private LayerMask mask;
    private void Awake()
    {
        boxSize = new Vector2(0.3f, 0.1f);
        maxDistance = 0.35f;
        controls = new InputControls();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private async void FixedUpdate()
    {
        if (canJump && controls.Controller.Jump.ReadValue<float>() >= 0.5f && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            canJump = false;
            await Timer(300);
        }
        isGrounded = GroundCheck();
        Vector2 movement = controls.Controller.Movement.ReadValue<Vector2>();
        Vector2 horizontalMovement;
        if(transform.position.y < -100)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        if (onLadder)
        {
            horizontalMovement = new Vector2(movement.x, 0);
            rb.gravityScale = 0;
            rb.velocity = horizontalMovement * movSpeed;
        }
        else if (!onLadder)
        {
            horizontalMovement = new Vector2(movement.x * movSpeed, rb.velocity.y);
            rb.gravityScale = 1;
            rb.velocity = horizontalMovement;
        }

        if (touchingLadder && !onLadder && controls.Controller.Interact.ReadValue<float>() >= 0.5f && isGrounded)
        {
            onLadder = true;
        }
        else if (touchingLadder && onLadder && controls.Controller.Jump.ReadValue<float>() >= 0.5f)
        {
            onLadder = false;
        }
        if (onLadder && movement.y > 0f)
        {
            transform.position += Vector3.up * ladderSpeed;
        }
        else if(onLadder && movement.y < 0f)
        {
            transform.position += Vector3.down * ladderSpeed;
        }
    }

    private async Task Timer(int timeInMilliseconds)
    {
        await Task.Delay(timeInMilliseconds);
        canJump = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
    private bool GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, mask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            touchingLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            touchingLadder = false;
            onLadder = false;
        }
    }
}
