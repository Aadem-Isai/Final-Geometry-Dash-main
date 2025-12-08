using UnityEngine;

public class LevelEnded : MonoBehaviour
{
    public string nextSceneName;

    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (completed) return;

        var player = other.GetComponent<Movement>();
        if (player != null)
        {
            completed = true;
            FindObjectOfType<GameManager>()?.PlayerDisappear(player.transform.position, player.transform, true, nextSceneName);
        }
    }
}

