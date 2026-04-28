using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TeleporterFinal : MonoBehaviour
{
    public string nomDeLaScene;
    public GameObject messageInteraction; 
    public GameObject objetDialogue;      
    public string nomDeLaCle = "Rusty Key"; 

    private bool estDansLaZone = false;
    private bool possedeLaCle = false;

    private void Update()
    {
        if (estDansLaZone && possedeLaCle && Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(nomDeLaScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estDansLaZone = true;
            if (Inventory.instance != null)
            {
                possedeLaCle = false;
                foreach (Item item in Inventory.instance.items)
                {
                    if (item != null && item.name == nomDeLaCle) { possedeLaCle = true; break; }
                }
            }

            if (possedeLaCle) {
                if (messageInteraction != null) messageInteraction.SetActive(true);
            } else {
                if (objetDialogue != null) objetDialogue.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estDansLaZone = false;
            if (messageInteraction != null) messageInteraction.SetActive(false);
            if (objetDialogue != null) objetDialogue.SetActive(false);
        }
    }
}