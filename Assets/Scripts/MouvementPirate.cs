using UnityEngine;

public class MouvementPirate : MonoBehaviour
{
    public Vector3 positionA;
    public Vector3 positionB;
    public Vector3 positionC;
    public Vector3 positionD;
    public float vitesse = 2f;
    
    private int etape = 0;

    void Update()
    {
        Vector3 cible = positionA;
        if (etape == 1) cible = positionB;
        if (etape == 2) cible = positionC;
        if (etape == 3) cible = positionD;

        transform.position = Vector3.MoveTowards(transform.position, cible, vitesse * Time.deltaTime);

        if (Vector3.Distance(transform.position, cible) < 0.1f)
        {
            etape++;
            
            if (etape > 3) 
            {
                etape = 0; 
            }
        }
    }
}