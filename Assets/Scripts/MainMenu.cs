using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // This loads the next scene in your build settings
        gameObject.SetActive(false); // hides the MainMenus object
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // This shows in the console to prove it works
        Application.Quit();
    }
}