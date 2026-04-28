using UnityEngine;
using Ilumisoft.HealthSystem;

public class WeaponDamage : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter(Collider other)
    {
        // Don't hit the player themselves
        if (other.CompareTag("Player")) return;

        var hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.ApplyDamage(damage);
        }
    }
}