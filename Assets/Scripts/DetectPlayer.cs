using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D DeathPlane;
    [SerializeField] private BoxCollider2D PotionCollisionBox;
    private SpriteRenderer spriteRenderer;
    private Vector2 startPos;
    private float speed;
    private bool isFalling = false;

    private void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private async Task ResetPotion()
    {
        await Task.Delay(5000);
        rb.isKinematic = false;
        rb.gravityScale = 0f;
        transform.position = startPos;
        spriteRenderer.enabled = true;
        PotionCollisionBox.enabled = true;
        DeathPlane.enabled = false;
    }

    private IEnumerator TempDeathPlane()
    {
        DeathPlane.enabled = true;
        yield return new WaitForSeconds(2);
        DeathPlane.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            rb.gravityScale = 1.0f;
            isFalling = true;
        }
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if(isFalling && collision.gameObject.tag != "Ladder")
        {
            rb.isKinematic = true;
            PotionCollisionBox.enabled = false;
            spriteRenderer.enabled = false;
            isFalling = false;
            StartCoroutine(TempDeathPlane());
            await ResetPotion();
        }
        else if (!isFalling && collision.gameObject.tag != "Ladder")
        {
            rb.gravityScale = 1.0f;
            isFalling = true;
        }
    }
}
