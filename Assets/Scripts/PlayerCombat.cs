using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play attack animation
        //detect enemies in range of attack
        //damage them
        Debug.Log("Player attacks!");
    }
}
