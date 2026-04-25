using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Collider hitbox;
    public float attackTime = 0.3f;
    private bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        hitbox.enabled = true;
        yield return new WaitForSeconds(attackTime);
        hitbox.enabled = false;

        yield return new WaitForSeconds(0.2f); // small cooldown
        isAttacking = false;
    }
}