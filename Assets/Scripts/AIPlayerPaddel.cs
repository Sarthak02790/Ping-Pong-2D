using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerPaddel : MonoBehaviour
{
    public Transform ball;
    public float speed = 8f;
    public float boundaryY = 4f;

    void Update()
    {
        if (ball != null)
        {
            float targetY = ball.position.y;
            Vector2 newPosition = transform.position;
            newPosition.y = Mathf.MoveTowards(transform.position.y, targetY, speed * Time.deltaTime);

            // Clamp the paddle's position within the game boundaries
            newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);
            transform.position = newPosition;
        }
    }
}