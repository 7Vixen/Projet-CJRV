using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TeleporterPORT_COULOIR : MonoBehaviour
{
    [Header("Réglages de la Scène")]
    public string nomDeLaScene;
    public float tempsAttente = 3.0f;

    [Header("Interface UI")]
    public GameObject ecranNoir;
    public Slider barreDeChargement;
    public Text texteLoading;
    public GameObject messageInteraction; // Glisse ici un texte "Appuyez sur T"

    private bool estDansLaZone = false;
    private bool transitionEnCours = false;

    private void Update()
    {
        // Si le joueur est dans la zone, qu'il appuie sur T et qu'on ne charge pas déjà
        if (estDansLaZone && Input.GetKeyDown(KeyCode.T) && !transitionEnCours)
        {
            transitionEnCours = true;
            if (messageInteraction != null) messageInteraction.SetActive(false);
            StartCoroutine(SequenceTransition());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !transitionEnCours)
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

    IEnumerator SequenceTransition()
    {
        if (ecranNoir != null) ecranNoir.SetActive(true);
        if (barreDeChargement != null) barreDeChargement.value = 0;
        if (texteLoading != null) texteLoading.text = "LOADING";

        float tempsEcoule = 0;

        while (tempsEcoule < tempsAttente)
        {
            tempsEcoule += Time.deltaTime;
            float progression = tempsEcoule / tempsAttente;

            if (texteLoading != null)
            {
                if (progression >= 0.75f) texteLoading.text = "LOADING . . .";
                else if (progression >= 0.50f) texteLoading.text = "LOADING . .";
                else if (progression >= 0.25f) texteLoading.text = "LOADING .";
            }

            if (barreDeChargement != null)
            {
                barreDeChargement.value = progression * 100;
            }

            yield return null;
        }

        SceneManager.LoadScene(nomDeLaScene);
    }
}