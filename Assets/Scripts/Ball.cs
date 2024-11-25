using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    public GameManager gameManager; // Reference to the GameManager script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Dynamically assign GameManager if not set in the Inspector
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        LaunchBall();
    }

    void LaunchBall()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(xDirection, yDirection).normalized;

        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerGoal"))
        {
            // AI scores
            gameManager.GoalScored(false); // false means it's not the player's goal
        }
        else if (collision.CompareTag("AIGoal"))
        {
            // Player scores
            gameManager.GoalScored(true); // true means the player scored
        }
    }
}
