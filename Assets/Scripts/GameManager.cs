using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public bool isPaused = false;
    public PlayerMovemnt playerMovement;

    void Update()
    {
        // Toggle inventory with the tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

     // drag in inspector

    public void ToggleInventory()
    {
        isPaused = !isPaused;
        inventoryCanvas.SetActive(isPaused);

        //playerMovement.enabled = !isPaused;
    }
}