using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterROOM_COULOIR : MonoBehaviour
{
    [Header("Réglages")]
    public string nomDeLaScene;
    public GameObject messageInteraction;
    private bool estDansLaZone = false;

    private void Update()
    {
        if (estDansLaZone && Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(nomDeLaScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estDansLaZone = true;
            if (messageInteraction != null) messageInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estDansLaZone = false;
            if (messageInteraction != null) messageInteraction.SetActive(false);
        }
    }
}