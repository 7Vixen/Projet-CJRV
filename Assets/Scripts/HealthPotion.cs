using UnityEngine;
using Ilumisoft.HealthSystem;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Health Potion")]
public class HealthPotion : Item
{
    public float healAmount = 25f;

    public override void Use()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            var health = player.GetComponent<HealthComponent>();
            if (health == null)
                health = player.GetComponentInChildren<HealthComponent>();
            if (health == null)
                health = player.GetComponentInParent<HealthComponent>();

            if (health != null)
                health.AddHealth(healAmount);
            else
                Debug.Log("HEALTH NOT FOUND ON PLAYER!");
        }
        else
        {
            Debug.Log("PLAYER GAMEOBJECT NOT FOUND!");
        }

        RemoveFromInventory();
    }
}