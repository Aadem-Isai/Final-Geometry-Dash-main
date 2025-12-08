using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // Reference to the pause menu UI
    private bool isPaused = false;

    // Removed Update() method - no ESC key needed

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);  // Show pause menu
        Time.timeScale = 0f;              // Freeze game time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;              // Resume normal time
        isPaused = false;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;              // Reset time before loading menu
        SceneManager.LoadScene("MainMenu"); // Load main menu scene
    }
}