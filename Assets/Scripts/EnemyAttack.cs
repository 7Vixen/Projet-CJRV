using UnityEngine;
using Ilumisoft.HealthSystem;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackRate = 1f;
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyDamageToPlayer(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            if (timer >= 1f / attackRate)
            {
                timer = 0f;
                ApplyDamageToPlayer(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0f;
    }

    private void ApplyDamageToPlayer(Collider other)
    {
        var health = other.GetComponent<HealthComponent>();
        if (health == null)
            health = other.GetComponentInParent<HealthComponent>();

        if (health != null)
        {
            Debug.Log("DAMAGING PLAYER: " + damage);
            health.ApplyDamage(damage);
        }
        else
        {
            Debug.Log("HEALTH NOT FOUND!");
        }
    }
}