using UnityEngine;
using Ilumisoft.HealthSystem;

public class DebugAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT: " + other.gameObject.name);

        var health = other.GetComponent<HealthComponent>();
        Debug.Log("Health on Cylinder: " + (health != null));

        var healthParent = other.GetComponentInParent<HealthComponent>();
        Debug.Log("Health on Parent: " + (healthParent != null));

        Debug.Log("Player root: " + other.transform.root.name);
    }
}