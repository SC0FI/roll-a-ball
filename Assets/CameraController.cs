using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject.
    public GameObject player;
    [SerializeField]
    [Range(10.0f, 360.0f)]
    private float rotationSpeed = 90.0f;

    // The distance between the camera and the player.
    private Vector3 offset;

    // Start is called before the first frame update.
    void Start()
    {
        // Calculate the initial offset between the camera's position and the player's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {
        // Maintain the same offset between the camera and player throughout the game.
        if (player == null) return;

        float rotationInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.qKey.isPressed)
                rotationInput -= 1f;

            if (Keyboard.current.eKey.isPressed)
                rotationInput += 1f;
        }

        float angle = rotationInput * rotationSpeed * Time.deltaTime;

        // Rotate the camera's position around the ball.
        offset = Quaternion.AngleAxis(angle, Vector3.up) * offset;

        transform.position = player.transform.position + offset;

        // Keep the camera looking at the ball.
        transform.LookAt(player.transform.position);
    }
}