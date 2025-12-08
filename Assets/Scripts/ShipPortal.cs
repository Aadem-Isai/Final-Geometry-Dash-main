using UnityEngine;

public class ShipPortal : MonoBehaviour
{
    public GameModes targetMode = GameModes.Ship;  // What mode to switch to
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movement movement = collision.GetComponent<Movement>();
            
            if (movement != null)
            {
                // Change to ship mode (State 1 = gamemode change)
                movement.ChangeThroughPortal(targetMode, movement.CurrentSpeed, Gravity.Normal, 1);
                
                Debug.Log("Player switched to: " + targetMode);
            }
        }
    }
}