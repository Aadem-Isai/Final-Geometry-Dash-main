using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Movement>();
        if (player != null)
        {
            FindObjectOfType<GameManager>()?.PlayerDisappear(player.transform.position, player.transform, false);
        }
    }
}
