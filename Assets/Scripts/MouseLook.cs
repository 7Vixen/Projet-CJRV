using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public Transform playerBody;
    public float xRotation = 0f;

    public static bool isPaused = false;

    // Inside MouseLook.cs
    void Start()
    {
        // REMOVE these lines:
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        
        // ADD this check so the camera only locks if we aren't in a menu
        if (!isPaused) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (isPaused) return;

        float mousex = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mousex);
    }
}