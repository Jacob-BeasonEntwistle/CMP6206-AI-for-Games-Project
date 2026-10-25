using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens = 0.1f;
    [SerializeField] private float maxMouseDelta = 50f;

    public Transform playerBody;
    public Transform cameraHolder;

    private float xRotation;
    private float standingHeight = 1.15f;
    private float crouchedHeight = 0.575f;
    private float crouchSpeed = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180f)
            xRotation -= 360f;
    }

    void Update()
    {
        if (Mouse.current == null || Keyboard.current == null)
            return;

        // Don't rotate while the game window is unfocused.
        if (!Application.isFocused ||
            Cursor.lockState != CursorLockMode.Locked)
            return;

        Vector2 delta = Mouse.current.delta.ReadValue();

        // Ignore unusually large input spikes.
        delta.x = Mathf.Clamp(delta.x, -maxMouseDelta, maxMouseDelta);
        delta.y = Mathf.Clamp(delta.y, -maxMouseDelta, maxMouseDelta);

        xRotation -= delta.y * mouseSens;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerBody != null)
            playerBody.Rotate(Vector3.up * delta.x * mouseSens);

        bool crouching =
            Keyboard.current.leftCtrlKey.isPressed ||
            Keyboard.current.cKey.isPressed;

        float targetHeight = crouching ? crouchedHeight : standingHeight;

        if (cameraHolder != null)
        {
            Vector3 pos = cameraHolder.localPosition;
            pos.y = Mathf.Lerp(
                pos.y, targetHeight, crouchSpeed * Time.deltaTime
            );

            if (Mathf.Abs(pos.y - targetHeight) < 0.01f)
                pos.y = targetHeight;

            cameraHolder.localPosition = pos;
        }
    }
}