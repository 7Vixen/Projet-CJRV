using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor; 

public class DetectionPirate : MonoBehaviour
{
    public Transform joueur;
    public float distanceLimite = 5f;
    
    [Header("Dialogues (Dialogue Editor)")]
    public NPCConversation failuremssg1; 
    public NPCConversation messageDebut; 

    private static bool doitAfficherEchec = false;

    void Start()
    {
        Invoke("GererDialogues", 0.1f);
    }

    void GererDialogues()
    {
        if (doitAfficherEchec)
        {
            if (failuremssg1 != null) ConversationManager.Instance.StartConversation(failuremssg1);
            doitAfficherEchec = false; 
        }
        else 
        {
            if (messageDebut != null) ConversationManager.Instance.StartConversation(messageDebut);
        }
    }

    void Update()
    {
        if (joueur == null) return;

        float distance = Vector3.Distance(transform.position, joueur.position);
        
        if (distance < distanceLimite)
        {
            LancerEchecInstantane();
        }
    }

    void LancerEchecInstantane()
    {
        doitAfficherEchec = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}