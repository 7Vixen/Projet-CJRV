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
            player.GetComponent<HealthComponent>()?.AddHealth(healAmount);
        }
        RemoveFromInventory();
    }
}