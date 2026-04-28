using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public bool isPaused = false;
    public PlayerMovemnt playerMovement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isPaused = !isPaused;
        inventoryCanvas.SetActive(isPaused);

        if (isPaused)
        {
            // Opening inventory — freeze camera, show cursor
            MouseLook.isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Closing inventory — unfreeze camera, hide cursor
            MouseLook.isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}