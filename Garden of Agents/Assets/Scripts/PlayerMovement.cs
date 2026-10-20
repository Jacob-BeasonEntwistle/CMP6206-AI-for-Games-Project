using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 2;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (velocity.y < 0)
        {
            velocity.y += gravity * 2.5f * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        float targetHeight;

        if (Input.GetButton("Crouch"))
        {
            targetHeight = crouchHeight;
        }
        else
        {
            targetHeight = defaultHeight;
        }

        controller.height = Mathf.Lerp(controller.height, targetHeight, crouchSpeed * Time.deltaTime);

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
