using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Animator animator;
    private States state = States.Walking;
    public enum States
    {
        Walking,
        Attacking,
        Dying,
        Hurting
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchState(States newState)
    {
        state = newState;
        Animate(state);
    }

    private void Animate(States state)
    {
        switch (state)
        {
            case States.Walking:
                animator.Play("Walk");
                break;
            case States.Dying:
                animator.Play("Death");
                break;
            case States.Attacking:
                animator.Play("Attack");
                break;
            case States.Hurting:
                animator.Play("Hurt");
                break;
        }
    }
}
