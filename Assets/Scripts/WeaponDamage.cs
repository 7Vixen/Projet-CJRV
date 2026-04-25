using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
    
}