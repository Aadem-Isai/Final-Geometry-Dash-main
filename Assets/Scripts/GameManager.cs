using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public ParticleSystem playerParticlePrefab;
    public float resetDelay = 1.5f;
    public UIManager uiManager;

    public void PlayerDisappear(Vector3 position, Transform playerTransform = null, bool levelComplete = false, string nextSceneName = "")
{
    if (playerParticlePrefab != null)
    {
        ParticleSystem ps = Instantiate(playerParticlePrefab, position, Quaternion.identity);
        if (playerTransform != null)
            ps.transform.localScale = Vector3.one * playerTransform.localScale.magnitude;
        ps.Play();
    }

    Movement player = FindObjectOfType<Movement>();
    if (player != null && player.gameObject != null)  // Added null check
    {
        player.gameObject.SetActive(false);
    }

    if (uiManager != null)
        uiManager.SetStatus(levelComplete ? "Level Complete!" : "You Died! Restarting...");

    if (levelComplete && !string.IsNullOrEmpty(nextSceneName))
        StartCoroutine(LoadNextSceneAfterDelay(nextSceneName, 2f));
    else if (!levelComplete)
        Invoke(nameof(ResetLevel), resetDelay);
}
    private void ResetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    private IEnumerator LoadNextSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
