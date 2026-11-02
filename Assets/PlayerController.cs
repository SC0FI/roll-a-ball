using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private GameLogic gameLogic;

    private InputValue movementValue;

    private float movementX;
    private float movementY;
    private bool dashRequested;
    private bool onGround;

    private int coins;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 1;

    [SerializeField]
    private Transform movementCamera;

    [SerializeField]
    private float dashSpeed = 10f;

    // Initialize physics before input callbacks can run.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.0f;

        if (movementCamera == null && Camera.main != null)
        {
            movementCamera = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {

        Vector3 forward = Vector3.forward;
        if (movementCamera != null)
        {
            // Ignore the camera's downward tilt so movement stays on the ground plane.
            forward = Vector3.ProjectOnPlane(movementCamera.forward, Vector3.up);
            if (forward.sqrMagnitude < 0.001f)
            {
                forward = Vector3.ProjectOnPlane(movementCamera.up, Vector3.up);
            }
            forward.Normalize();
        }

        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 movement = Vector3.ClampMagnitude(right * movementX + forward * movementY, 1f);
        rb.AddForce(movement * speed);

        if (dashRequested)
        {
            // Dash toward the input, or camera-forward when standing still.
            Vector3 dashDirection = movement.sqrMagnitude > 0f ? movement.normalized : forward;
            rb.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);
            dashRequested = false;
        }
    }


    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue movementValue)
    {
        if (movementValue.isPressed && onGround)
        {
            rb.AddForce(Vector3.up * rb.mass * 500);
        }
    }

    void Update()
    {
        if (Keyboard.current != null &&
            (Keyboard.current.leftShiftKey.wasPressedThisFrame || Keyboard.current.rightShiftKey.wasPressedThisFrame))
        {
            dashRequested = true;
        }

        if (transform.position.y <= 0.5) { onGround = true; } else { onGround = false; }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Use the Rotator belonging to the pickup we actually touched.
            if (!other.TryGetComponent(out Rotator pickup))
            {
                Debug.LogError("Pickup is missing a Rotator component.", other);
                return;
            }

            pickup.touched(other.gameObject);
            gameLogic.AddCoin();
            gameLogic.SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            gameLogic.EndGame("Lose");
        }
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        } else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    void OnPause()
    {
        gameLogic.Pause();
    }

}
