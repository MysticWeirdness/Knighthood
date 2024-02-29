using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeManager : MonoBehaviour
{
    MovementHandler movementHandler;
    [SerializeField] SnakesStateMachine snakesStateMachine;

    [SerializeField] private LayerMask mask;
    private void Start()
    {
        movementHandler = GetComponent<MovementHandler>();
    }
    void Attack()
    {
        snakesStateMachine.SwitchState(SnakesStateMachine.State.attacking);
    }

    public void HurtPlayer()
    {
        snakesStateMachine.SwitchState(SnakesStateMachine.State.walking);
        if (Physics2D.Raycast(transform.position, Vector2.right * movementHandler.movDir, 1f, mask))
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        }
    }
    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right * movementHandler.movDir, 1f, mask))
        {
            Attack();
        }
    }
}
