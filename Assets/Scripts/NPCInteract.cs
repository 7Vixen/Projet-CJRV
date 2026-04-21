using DialogueEditor;
using UnityEngine;

public class NPCInteract : MonoBehaviour,IInteractable
{
    [SerializeField] private NPCConversation myConversavtion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        ConversationManager.Instance.StartConversation(myConversavtion);
}
}
