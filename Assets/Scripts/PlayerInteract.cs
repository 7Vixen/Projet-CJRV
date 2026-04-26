using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2f;
    private List<IInteractable> nearby = new List<IInteractable>();

    private void Update()
{
    nearby.Clear();

    Collider[] colliders = Physics.OverlapSphere(
        transform.position,
        interactRange,
        ~0,
        QueryTriggerInteraction.Collide
    );

    foreach (Collider col in colliders)
    {
        if (col.TryGetComponent(out IInteractable interactable))
        {
            nearby.Add(interactable);
        }
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
        foreach (var i in nearby)
        {
            i.Interact();
            break;
        }
    }
}
    public IInteractable GetInteractableObject()
    {
    Collider[] colliders = Physics.OverlapSphere(
        transform.position,
        interactRange,
        ~0,
        QueryTriggerInteraction.Collide
    );

    foreach (Collider col in colliders)
    {
        if (col.TryGetComponent(out IInteractable interactable))
        {
            return interactable;
        }
    }

    return null;
}
}