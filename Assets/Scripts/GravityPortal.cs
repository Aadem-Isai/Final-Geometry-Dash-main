using UnityEngine;

public class GravityPortal : MonoBehaviour
{
    public Transform arrowIndicator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            Movement movement = collision.GetComponent<Movement>();
            
            if (playerRb != null && movement != null)
            {
                // Determine new gravity state (flip current state)
                // Check current gravity from either rb.gravityScale OR isGravityInverted flag
                bool currentlyInverted = movement.isGravityInverted;
                bool newInverted = !currentlyInverted;
                
                // Use the portal system to change gravity
                Gravity gravityEnum = newInverted ? Gravity.Inverted : Gravity.Normal;
                movement.ChangeThroughPortal(movement.CurrentGameMode, movement.CurrentSpeed, gravityEnum, 2);
                
                // Flip the player sprite upside down
                if (movement.Sprite != null)
                {
                    Vector3 spriteScale = movement.Sprite.localScale;
                    spriteScale.y *= -1;
                    movement.Sprite.localScale = spriteScale;
                }
                
                // Flip GroundCheck (only matters for cube, but flip anyway)
                if (movement.GroundCheckTransform != null)
                {
                    Vector3 groundCheckPos = movement.GroundCheckTransform.localPosition;
                    groundCheckPos.y *= -1;
                    movement.GroundCheckTransform.localPosition = groundCheckPos;
                }
                
                // Force lock rotation
                playerRb.angularVelocity = 0f;
                playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
                
                Debug.Log("Gravity flipped. New inverted state: " + movement.isGravityInverted);
            }
        }
    }
}