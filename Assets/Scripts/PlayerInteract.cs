using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

            foreach (Collider col in colliders)
            {
                if (col.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                    break;
                }
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

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