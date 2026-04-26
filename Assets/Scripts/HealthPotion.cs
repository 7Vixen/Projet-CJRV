using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Health Potion")]
public class HealthPotion : Item
{
    public float healAmount = 25f;

public override void Use()
{
    Debug.Log("POTION USED");
    Health.instance.Heal(healAmount);
    RemoveFromInventory();
}
}