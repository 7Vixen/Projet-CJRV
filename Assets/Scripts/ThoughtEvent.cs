using UnityEngine;
using TMPro;
using System.Collections;

public class ThoughtEvent : MonoBehaviour
{
    public GameObject textObject;
    public float displayTime = 4f;

    void Start()
    {
        StartCoroutine(ShowThought());
    }

    IEnumerator ShowThought()
    {
        textObject.SetActive(true);
        
        yield return new WaitForSeconds(displayTime);
        
        textObject.SetActive(false);
    }
}