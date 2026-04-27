using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;

    public float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Healed! HP: " + currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // ─── PAIN SOUND ───────────────────────────────────────────
        AudioManager.Instance.PlayPainScream();
        // ─────────────────────────────────────────────────────────

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ─── VICTORY MUSIC ────────────────────────────────────────
        // (called here only if THIS is the enemy — move to a 
        //  GameManager if you want it on player win condition)
        // AudioManager.Instance.PlayVictoryMusic();
        // ─────────────────────────────────────────────────────────

        Destroy(gameObject);
    }
}