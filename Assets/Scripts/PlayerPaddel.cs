using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddel : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryY = 4f;
    public bool isPlayerOne = true; // Toggle for Player 1 or Player 2

    void Update()
    {
        // Use "Vertical" input for Player 1 and "Vertical2" for Player 2
        float input = isPlayerOne ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical2");

        Vector2 newPosition = transform.position;
        newPosition.y += input * speed * Time.deltaTime;

        // Clamp the paddle's position within the game boundaries
        newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);
        transform.position = newPosition;
    }
}