using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private State state = State.idle;

    public enum State
    {
        idle,
        walking,
        running,
        attacking,
        jumping
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SwitchState(State newState)
    {
        state = newState;
        Animate(state);
    }

    public State GetCurrentState()
    {
        return state;
    }
    public void ChangeSpriteDirection(float horizontalMovement)
    {
        if (horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Update()
    {
        Debug.Log(state);
    }
    private void Animate(State state)
    {
        switch (state)
        {
            case State.idle:
                animator.Play("Idle");
                break;
            case State.walking:
                animator.Play("Walk");
                break;
            case State.running:
                animator.Play("Run");
                break;
            case State.attacking:
                animator.Play("Attack");
                break;
            case State.jumping:
                animator.Play("Jump");
                break;
        }
    }
}
