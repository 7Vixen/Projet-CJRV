using UnityEngine;

public class NPCLoopControl : MonoBehaviour
{
    public float walkDuration = 5.0f; // How many seconds they walk
    private float timer;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        timer = walkDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Time is up! Snap them back and restart the clock
            transform.position = startPosition;
            timer = walkDuration;
            
            // Optional: Re-trigger the animation from the start
            GetComponent<Animator>().Play("Walk", 0, 0f);
        }
    }
}