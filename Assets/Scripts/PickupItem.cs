using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public Item item;

    public void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}