using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Get the contact point to determine direction of hit
            ContactPoint2D contact = collision.GetContact(0);
            
            // contact.normal tells us the direction of the collision
            // If hit from SIDE: normal.x will be close to 1 or -1
            // If hit from TOP: normal.y will be close to 1
            // If hit from BOTTOM: normal.y will be close to -1
            
            // Check if hit from the side (horizontal collision)
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                // Side hit - kill player
                Debug.Log("Side collision detected - killing player");
                GameManager gm = FindObjectOfType<GameManager>();
                if (gm != null)
                {
                    gm.PlayerDisappear(collision.transform.position, collision.transform);
                }
            }
            else
            {
                // Top/bottom hit - safe, let player land or bounce
                Debug.Log("Top/bottom collision - player safe");
            }
        }
    }
}