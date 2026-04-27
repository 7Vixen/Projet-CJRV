using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Collider hitbox;
    public float attackTime = 0.3f;
    private bool isAttacking = false;

    private PlayerMovemnt playerMovemnt;

    void Start()
    {
        playerMovemnt = GetComponent<PlayerMovemnt>();
    }

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

        // ─── ATTACK SOUNDS ────────────────────────────────────────
        if (playerMovemnt != null && playerMovemnt.swordModel.activeInHierarchy)
            AudioManager.Instance.PlaySwordSwing();
        else
            AudioManager.Instance.PlayPunch();
        // ─────────────────────────────────────────────────────────

        hitbox.enabled = true;
        yield return new WaitForSeconds(attackTime);
        hitbox.enabled = false;

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }
}