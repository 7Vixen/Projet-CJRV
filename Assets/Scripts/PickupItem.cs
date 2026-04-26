using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    // Drag your "arms 1" object into this slot in the Inspector
    public Animator playerAnim; 

            public void Interact()
        {
            Debug.Log("Picked up item!");

            // Find the animator on the arms
            GameObject arms = GameObject.Find("arms 1");
            if (arms != null)
            {
                Animator anim = arms.GetComponent<Animator>();
                // This turns the "hasItem" bool to True in the animator
                anim.SetBool("hasItem", true);
            }

            // Later, you can add code here to actually spawn a potion model in the hand
            Destroy(gameObject);
        }
}