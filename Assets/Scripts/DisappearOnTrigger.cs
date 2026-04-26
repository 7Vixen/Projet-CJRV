using UnityEngine;
using DialogueEditor; // <--- AJOUTE ÇA

public class DisappearOnTrigger : MonoBehaviour
{
    public GameObject objectToHide;
    public NPCConversation monDialogue; // <--- AJOUTE ÇA (La variable pour Amina)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Lance le dialogue Amina
            if (monDialogue != null)
            {
                ConversationManager.Instance.StartConversation(monDialogue);
            }

            // 2. Cache le pirate
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }
            
            Debug.Log("Le pirate a disparu et le dialogue est lancé !");
        }
    }
}