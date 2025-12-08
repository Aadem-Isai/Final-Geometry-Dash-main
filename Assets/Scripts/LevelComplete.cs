using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject levelCompleteCanvas;
    public ParticleSystem completionParticles;  // Optional
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hide THIS object (the End sprite itself)
            gameObject.SetActive(false);
            
            // Play completion particles at player position
            if (completionParticles != null)
            {
                completionParticles.transform.position = collision.transform.position;
                completionParticles.Play();
            }
            
            // Hide player
            collision.gameObject.SetActive(false);
            
            // Show level complete menu after a short delay
            Invoke(nameof(ShowCompletionMenu), 0.5f);
            
            Debug.Log("Level Complete!");
        }
    }
    
    private void ShowCompletionMenu()
    {
        if (levelCompleteCanvas != null)
        {
            levelCompleteCanvas.SetActive(true);
        }
        
        // Stop time
        Time.timeScale = 0f;
    }
    
    // Button functions
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}