using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private State currentState = State.idle;

    public enum State
    {
        idle,
        walking,
        running,
        attacking,
        jumping,
        climbing,
        falling
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SwitchState(State.idle);
    }
    public void SwitchState(State newState)
    {
        currentState = newState;
        Animate(currentState);
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    private void Animate(State state)
    {
        switch (state)
        {
            case State.idle:
                animator.Play(Animator.StringToHash("Idle"));
                break;
            case State.walking:
                animator.Play(Animator.StringToHash("Walk"));
                break;
            case State.running:
                animator.Play(Animator.StringToHash("Run"));
                break;
            case State.attacking:
                animator.Play(Animator.StringToHash("Attack"));
                break;
            case State.jumping:
                animator.Play(Animator.StringToHash("Jump"));
                break;
            case State.climbing:
                animator.Play(Animator.StringToHash("Climb"));
                break;
            case State.falling:
                animator.Play(Animator.StringToHash("Fall"));
                break;
        }
    }
}
