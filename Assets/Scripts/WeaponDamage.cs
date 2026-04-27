using UnityEngine;
using Ilumisoft.HealthSystem;

public class WeaponDamage : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter(Collider other)
    {
        var hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.ApplyDamage(damage);
        }
    }
}