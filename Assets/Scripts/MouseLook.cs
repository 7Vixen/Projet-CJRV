using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity=100f;
    public Transform playerBody;
    public float xRotation=0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousex=Input.GetAxis("Mouse X")*MouseSensitivity*Time.deltaTime;
        float mousey=Input.GetAxis("Mouse Y")*MouseSensitivity*Time.deltaTime;
        xRotation-=mousey;
        xRotation=Mathf.Clamp(xRotation,-90f,90f);
        transform.localRotation=Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up*mousex);
    }
}
