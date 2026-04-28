using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmbientZone : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeInDuration  = 2f;
    public float fadeOutDuration = 2f;

    void Awake()
    {
        // Make sure the collider is a trigger
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AudioManager.Instance.FadeInCrowd(fadeInDuration);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            AudioManager.Instance.FadeOutCrowd(fadeOutDuration);
    }
}