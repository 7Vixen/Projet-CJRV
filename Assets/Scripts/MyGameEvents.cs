using UnityEngine;

public class MyGameEvents : MonoBehaviour
{
    public void MakePirateDisappear(GameObject pirate)
    {
        if (pirate != null)
        {
            pirate.SetActive(false);
            Debug.Log("L'événement : Le pirate a disparu !");
        }
    }
}