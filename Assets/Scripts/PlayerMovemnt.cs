using System;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    public CharacterController controller; 
    public Animator anim;
    public float speed = 12f;
    public float gravity = -9.8f;
    public Transform GroundCheck; 
    public float GroundDistance = 0.4f; 
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight = 3f;
    Vector3 Velocity;
    public GameObject swordModel; 
    public GameObject potionModel;

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);

        float currentSpeed = new Vector2(x, z).magnitude;
        anim.SetFloat("Speed", currentSpeed);

        // ─── FOOTSTEP SOUNDS ──────────────────────────────────────
        if (currentSpeed > 0.1f && isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                AudioManager.Instance.PlayRunning();
            else
                AudioManager.Instance.PlayWalking();
        }
        else
        {
            AudioManager.Instance.StopFootsteps();
        }
        // ─────────────────────────────────────────────────────────

        if (Input.GetButtonDown("Jump"))
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        Velocity.y += gravity * Time.deltaTime; 
        controller.Move(Velocity * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            if (swordModel.activeInHierarchy)
                anim.SetTrigger("Swing"); 
            else
                anim.SetTrigger("Punch"); 
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipSword();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EquipPotion();
        }
    }

    void EquipSword()
    {
        if (swordModel != null && anim != null)
        {
            bool isAlreadyEquipped = swordModel.activeSelf;

            if (isAlreadyEquipped)
            {
                swordModel.SetActive(false);
                anim.SetBool("hasSword", false);
            }
            else
            {
                potionModel.SetActive(false); 
                anim.SetBool("hasPotion", false);

                swordModel.SetActive(true);
                anim.SetBool("hasSword", true);

                // ─── SWORD DRAW SOUND ─────────────────────────────
                AudioManager.Instance.PlaySwordDraw();
                // ─────────────────────────────────────────────────
            }
        }
    }

    void EquipPotion()
    {
        if (potionModel != null && anim != null)
        {
            bool isAlreadyEquipped = potionModel.activeSelf;

            if (isAlreadyEquipped)
            {
                potionModel.SetActive(false);
                anim.SetBool("hasPotion", false);
            }
            else
            {
                swordModel.SetActive(false);
                anim.SetBool("hasSword", false);

                potionModel.SetActive(true);
                anim.SetBool("hasPotion", true);
            }
        }
    }
}