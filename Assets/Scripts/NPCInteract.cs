using DialogueEditor;
using UnityEngine;

public class NPCInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCConversation myConversavtion;

    public void Interact()
    {
        MouseLook.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ConversationManager.Instance.StartConversation(myConversavtion);
    }
}