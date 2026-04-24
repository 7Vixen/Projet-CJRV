using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2f;
    // We store the reference once so the computer doesn't have to "search" every frame
    public Animator armsAnimator; 

    private void Update()
    {
        // 1. HANDLE INTERACTION (Press E)
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

        // 2. HANDLE ATTACK/SWING (Left Click)
        // Only run this if we actually have an animator linked
        if (Input.GetMouseButtonDown(0) && armsAnimator != null) 
        {
            // This sends the "Swing" signal to your Animator
            armsAnimator.SetTrigger("Swing");
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