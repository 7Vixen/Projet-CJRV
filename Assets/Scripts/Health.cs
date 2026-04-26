using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance; // ← Add singleton

    public float maxHealth = 100f;
    private float currentHealth;

    private void Awake() // ← Use Awake for singleton
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amount) // ← Add this
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Healed! HP: " + currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}