using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Teleporteur : MonoBehaviour
{
    [Header("Réglages de la Scène")]
    public string nomDeLaScene;
    public float tempsAttente = 3.0f;

    [Header("Interface UI")]
    public GameObject ecranNoir;
    public Slider barreDeChargement;
    public Text texteLoading;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SequenceTransition());
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
                    if (progression >= 0.75f) {
                        texteLoading.text = "LOADING . . .";
                    }
                    else if (progression >= 0.50f) {
                        texteLoading.text = "LOADING . .";
                    }
                    else if (progression >= 0.25f) {
                        texteLoading.text = "LOADING .";
                    }
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