using System;
using UnityEngine;



public class PlayerMovemnt : MonoBehaviour
{
    public CharacterController controller; 
    public Animator anim; // --- ADD THIS LINE TO LINK YOUR ARMS ---
    public float speed=12f;
    public float gravity=-9.8f;
    public Transform GroundCheck; 
    public float GroundDistance=0.4f; 
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight=3f;
    Vector3 Velocity;
    public GameObject swordModel; 
    

    void Update()
    {
        isGrounded=Physics.CheckSphere(GroundCheck.position,GroundDistance,groundMask);
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y=-2f;
        }

        float x=Input.GetAxis("Horizontal");
        float z=Input.GetAxis("Vertical");
        Vector3 move=transform.right*x + transform.forward *z;
        
        controller.Move(move*speed*Time.deltaTime);

        // --- ADD THESE TWO LINES HERE ---
        // This calculates how fast you are moving based on your Horizontal and Vertical input
        float currentSpeed = new Vector2(x, z).magnitude; 
        anim.SetFloat("Speed", currentSpeed); // This tells the Animator to switch clips
        // --------------------------------

        if (Input.GetButtonDown("Jump"))
        {
            Velocity.y=Mathf.Sqrt(jumpHeight * -2f *gravity);
        }
        
        Velocity.y+=gravity*Time.deltaTime; 
        controller.Move(Velocity*Time.deltaTime);

        // Check for Left Mouse Button click (0 is left, 1 is right)
        if (Input.GetMouseButtonDown(0))
        {
            // This "rings the bell" for the Swing trigger
            anim.SetTrigger("Swing");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) // The "1" key
        {
            EquipSword();
        }
    }

           void EquipSword()
        {
            if (swordModel != null && anim != null)
            {
                // This checks if the sword is currently active
                bool isAlreadyEquipped = swordModel.activeSelf;

                if (isAlreadyEquipped)
                {
                    // PUT AWAY: Turn off model and reset fingers
                    swordModel.SetActive(false);
                    anim.SetBool("hasSword", false);
                    Debug.Log("Sword Put Away");
                }
                else
                {
                    // EQUIP: Turn on model and curl fingers
                    swordModel.SetActive(true);
                    anim.SetBool("hasSword", true);
                    Debug.Log("Sword Equipped");
                }
            }
        }
}