using System;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    public CharacterController controller; //monitor that drives our player to move 
    public float speed=12f;
    public float gravity=-9.8f;
    public Transform GroundCheck;//refernce 3la objet 
    public float GroundDistance=0.4f;//hada ra7 ykon radius ta3 sphere ta3na 
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight=3f;
    Vector3 Velocity;

    // Update is called once per frame
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
        if (Input.GetButtonDown("Jump"))
        {
            Velocity.y=Mathf.Sqrt(jumpHeight * -2f *gravity);

        }
        Velocity.y+=gravity*Time.deltaTime; //deltaY=1/2 g t^2  donc basically ki n7ab natl3 f y nadrbha b force or gravity b negtaive bach nti7 f lard 
        controller.Move(Velocity*Time.deltaTime);
    }
}
