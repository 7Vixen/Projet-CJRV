using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Touches Q/D ou Flèches
        float moveZ = Input.GetAxis("Vertical");   // Touches Z/S ou Flèches

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }
}