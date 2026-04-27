using UnityEngine;
using UnityEngine.UI;
using Ilumisoft.HealthSystem;

public class HUDManager : MonoBehaviour
{
    [Header("Player")]
    public HealthComponent playerHealth;
    public Image playerFill;

    [Header("Enemy")]
    public HealthComponent enemyHealth;
    public Image enemyFill;

    void Start()
    {
        Debug.Log("Player Health found: " + (playerHealth != null));
        Debug.Log("Player Max HP: " + (playerHealth != null ? playerHealth.MaxHealth : 0));
        Debug.Log("Fill Image found: " + (playerFill != null));
    }

    void Update()
    {
        if (playerHealth != null && playerFill != null)
            playerFill.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;

        if (enemyHealth != null && enemyFill != null)
            enemyFill.fillAmount = enemyHealth.CurrentHealth / enemyHealth.MaxHealth;
    }
}