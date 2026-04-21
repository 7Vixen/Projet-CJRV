using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Picked up item!");

        // later: add inventory system here
        Destroy(gameObject);
    }
}