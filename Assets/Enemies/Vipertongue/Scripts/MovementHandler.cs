using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private bool right = true;
    private float movDir = 1;
    private float movSpeed = 150;
    [SerializeField] private LayerMask mask;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (!right)
        {
            ToggleDirection();
        }
    }
    void Update()
    {
        CheckForCollision();
        rb2d.velocity = new Vector2(movDir * movSpeed * Time.deltaTime, rb2d.velocity.y);      
    }

    private void CheckForCollision()
    {
        if(Physics2D.Raycast(transform.position, Vector2.right * movDir, 1f, mask))
        {
            ToggleDirection();
        }
    }
    private void ToggleDirection()
    {
        movDir = -movDir;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
