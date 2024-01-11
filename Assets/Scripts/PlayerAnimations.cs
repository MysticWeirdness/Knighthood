using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    private enum state
    {
        idle,
        walking,
        running,
        attacking,
        jumping
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
    }
}
