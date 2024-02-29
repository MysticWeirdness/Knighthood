using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int health = 3;
    private int lives = 3;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    public void TakeDamage(int amt)
    {
        health -= amt;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        lives--;
        health = 3;
        if(lives <= 0)
        {
            GameOver();
        }
        Destroy(gameObject);
    }

    private void GameOver()
    {
        gameController.GameOverUI();
    }
}
