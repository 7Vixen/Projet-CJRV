using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        isOpen = !isOpen;

        if (isOpen)
            transform.Rotate(0, 90, 0);
        else
            transform.Rotate(0, -90, 0);
    }
}