using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        // 1. Déclenche le fondu de la musique via l'AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StartFadeOut(1.5f);
        }

        // 2. Paramètres du curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // 3. Débloque la caméra
        MouseLook.isPaused = false;

        // 4. Désactive le menu
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}