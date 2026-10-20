using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSens = 250f;

    public Transform playerBody;
    public Transform cameraHolder;

    private float standingHeight = 1.15f;
    private float crouchedHeight = 0.575f;

    private float crouchSpeed = 10f;

    // Stores manual rotation
    private float xRotation;
    private float yRotation;

    private bool justLocked;
    private bool ignoreNextFrame;

    // Start is called before the first frame update
    void Start()
    {
        // Unlock the cursor on start
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Store the initial camera rotation
        xRotation = transform.localEulerAngles.x;

        if (xRotation > 180f)
        {
            xRotation -= 360f;
        }

        // Store initial player rotation
        yRotation = playerBody.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Ignore the first frame after regaining focus
        if (ignoreNextFrame)
        {
            ignoreNextFrame = false;
            return;
        }

        // Lock the cursor to the screen on click (LMB)
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            justLocked = true;
        }

        // Unlock cursor with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // Ignore first frame after locking
        if (justLocked)
        {
            justLocked = false;
            return;
        }

        // Read the raw mouse movement in x and y axis
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens * Time.deltaTime;

        // Ignore huge spikes in mouse movements
        if (Mathf.Abs(mouseX) > 20f || Mathf.Abs(mouseY) > 20f)
        {
            return;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        yRotation += mouseX;
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        float targetHeight;

        if (Input.GetButton("Crouch"))
        {
            targetHeight = crouchedHeight;
        }
        else
        {
            targetHeight = standingHeight;
        }

        float newHeight = Mathf.Lerp(cameraHolder.localPosition.y, targetHeight, crouchSpeed * Time.deltaTime);

        if (Mathf.Abs(newHeight - targetHeight) < 0.01f)
        {
            newHeight = targetHeight;
        }

        cameraHolder.localPosition = new Vector3(0f, newHeight, 0f);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            ignoreNextFrame = true;
        }
    }
}
