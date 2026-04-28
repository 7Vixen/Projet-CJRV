using DialogueEditor;
using UnityEngine;

public class ConversationEnd : MonoBehaviour
{
    private bool wasActive = false;

    void Update()
    {
        bool isActive = ConversationManager.Instance.IsConversationActive;

        if (wasActive && !isActive)
        {
            MouseLook.isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        wasActive = isActive;
    }
}