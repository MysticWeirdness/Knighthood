using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Animator animator;

    private enum States
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
}
