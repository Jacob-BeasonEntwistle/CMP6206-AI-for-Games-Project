using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private float defaultHeight;
    private float crouchHeight;
    private float crouchSpeed;

    public float speed;
    public float jumpHeight;
    public float gravity;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private Vector3 velocity;
    public bool isGrounded;

    void Start()
    {
        speed = 3.0f;
        jumpHeight = 1.0f;
        gravity = -9.81f;
        groundDistance = 0.4f;

        defaultHeight = controller.height;
        crouchHeight = defaultHeight / 2f;
        crouchSpeed = 10f;
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // WASD movement
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed) x -= 1f;
        if (Keyboard.current.dKey.isPressed) x += 1f;
        if (Keyboard.current.sKey.isPressed) z -= 1f;
        if (Keyboard.current.wKey.isPressed) z += 1f;

        Vector3 move = transform.right * x + transform.forward * z;

        // Prevent faster diagonal movement
        move = Vector3.ClampMagnitude(move, 1f);

        float currentSpeed = speed;

        // Sprint
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed *= 2f;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            velocity.y = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );
        }

        // Gravity
        if (velocity.y < 0)
        {
            velocity.y += gravity * 2.5f * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Crouch with Left Ctrl or C
        bool crouching = Keyboard.current.leftCtrlKey.isPressed || Keyboard.current.cKey.isPressed;

        float targetHeight = crouching ? crouchHeight : defaultHeight;

        controller.height = Mathf.Lerp(
            controller.height,
            targetHeight,
            crouchSpeed * Time.deltaTime
        );

        if (Mathf.Abs(controller.height - targetHeight) < 0.01f)
        {
            controller.height = targetHeight;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
