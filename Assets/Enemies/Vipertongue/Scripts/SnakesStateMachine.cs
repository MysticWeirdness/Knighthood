using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakesStateMachine : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private State currentState;

    public enum State
    {
        walking,
        attacking
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SwitchState(State.walking);
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
            case State.walking:
                animator.Play(Animator.StringToHash("Walk"));
                break;
            case State.attacking:
                animator.Play(Animator.StringToHash("Attack"));
                break;
        }
    }
}
