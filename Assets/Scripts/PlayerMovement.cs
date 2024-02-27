using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditorInternal;

public class PlayerMovement : MonoBehaviour
{
    private InputControls controls;
    private Rigidbody2D rb;
    private PlayerAnimations stateMachine;

    private float movSpeed = 2f;
    private bool onLadder = false;
    private bool touchingLadder = false;
    [SerializeField] private bool isGrounded = true;
    private bool canJump = true;
    private bool attacking = false;
    private float jumpForce = 5f; 
    private float ladderSpeed = 0.05f;

    private Vector3 facingRight;
    private Vector3 facingLeft;

    private Vector3 boxSize;
    private float maxDistance;
    [SerializeField] private LayerMask mask;
    private void Awake()
    {
        facingRight = new Vector3(1, 1, 1);
        facingLeft = new Vector3(-1, 1, 1);
        boxSize = new Vector2(0.3f, 0.1f);
        maxDistance = 0.35f;
        controls = new InputControls();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponentInChildren<PlayerAnimations>();
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
            await JumpTimer(300);
        }
        isGrounded = GroundCheck();
        Vector2 movement = controls.Controller.Movement.ReadValue<Vector2>();
        
        if(movement == Vector2.zero && isGrounded && !onLadder && !attacking)
        {
            stateMachine.SwitchState(PlayerAnimations.State.idle);
        }
        else if(movement.x != 0 && isGrounded && !onLadder && controls.Controller.Run.ReadValue<float>() < 0.5f)
        {
            movSpeed = 2f;
            stateMachine.SwitchState(PlayerAnimations.State.walking);
        }
        else if (movement.x != 0 && isGrounded && !onLadder && controls.Controller.Run.ReadValue<float>() >= 0.5f)
        {
            movSpeed = 3f;
            stateMachine.SwitchState(PlayerAnimations.State.running);
        }
        else if (rb.velocity.y > 0 && !isGrounded && !onLadder)
        {
            stateMachine.SwitchState(PlayerAnimations.State.jumping);
        }
        else if(rb.velocity.y < 0 && !isGrounded && !onLadder)
        {
            stateMachine.SwitchState(PlayerAnimations.State.falling);
        }

        if (movement.x > 0)
        {
            transform.localScale = facingRight;
        }
        else if(movement.x < 0)
        {
            transform.localScale = facingLeft;
        }
        if(controls.Controller.Attack.ReadValue<float>() >= 0.5f && stateMachine.GetCurrentState() == PlayerAnimations.State.idle)
        {
            attacking = true;
            await AttackTimer(1000);
            stateMachine.SwitchState(PlayerAnimations.State.attacking);
        }
        Vector2 horizontalMovement;
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

        if (touchingLadder && !onLadder && controls.Controller.Interact.ReadValue<float>() >= 0.5f)
        {
            onLadder = true;
            stateMachine.SwitchState(PlayerAnimations.State.climbing);
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
            if (isGrounded)
            {
                onLadder = false;
            }
            else
            {
                transform.position += Vector3.down * ladderSpeed;
            }
        }
    }

    private async Task AttackTimer(int timeInMilliseconds)
    {
        await Task.Delay(timeInMilliseconds);
        attacking = false;
    }
    private async Task JumpTimer(int timeInMilliseconds)
    {
        await Task.Delay(timeInMilliseconds);
        canJump = true;
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
