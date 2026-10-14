using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSens = 1000f;

    public Transform playerBody;
    public Transform cameraHolder;

    private float standingHeight = 1.15f;
    private float crouchedHeight = 0.575f;

    private float crouchSpeed = 10f;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

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
}
