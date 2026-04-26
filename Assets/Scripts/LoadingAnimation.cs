using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingAnimation : MonoBehaviour
{
    public Text texteLoading;
    public string phraseDeBase = "LOADING";
    public float vitesse = 0.4f;

    private bool estEnTrainDAnimer = false;

    public void LancerAnimation()
    {
        if (!estEnTrainDAnimer)
        {
            estEnTrainDAnimer = true;
            StartCoroutine(AnimerPoints());
        }
    }

    public void ArreterAnimation()
    {
        estEnTrainDAnimer = false;
        StopAllCoroutines();
    }

    IEnumerator AnimerPoints()
    {
        while (estEnTrainDAnimer)
        {
            texteLoading.text = phraseDeBase;
            yield return new WaitForSeconds(vitesse);
            texteLoading.text = phraseDeBase + ".";
            yield return new WaitForSeconds(vitesse);
            texteLoading.text = phraseDeBase + "..";
            yield return new WaitForSeconds(vitesse);
            texteLoading.text = phraseDeBase + "...";
            yield return new WaitForSeconds(vitesse);
        }
    }
}