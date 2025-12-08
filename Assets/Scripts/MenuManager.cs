using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // This function loads a scene by its name
    public void LoadLevel(string levelName)
    {
        // Load the scene
        SceneManager.LoadScene(levelName);
    }

    // Optional: Quit game function (for later if you want a quit button)
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
