using Unity.VisualScripting;
using UnityEngine;

public class PlyerInterecUI : MonoBehaviour
{
    [SerializeField] private GameObject ContainerGameObject;
    [SerializeField] private PlayerInteract playerInteract; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            show();
        }
        else
        {
            Hide();
        }
    }
    private void show()
    {
        ContainerGameObject.SetActive(true);
    }
    private void Hide()
    {
        ContainerGameObject.SetActive(false);
    }
}
