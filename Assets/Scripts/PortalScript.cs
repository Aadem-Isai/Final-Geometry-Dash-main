using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PortalScript : MonoBehaviour
{
    public GameModes Gamemode;
    public Speeds Speed;
    public Gravity gravity;
    public int State;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Portal triggered! Changing to Speed: {Speed}");
        
        Movement movement = collision.gameObject.GetComponent<Movement>();
        if (movement != null)
        {
            movement.ChangeThroughPortal(Gamemode, Speed, gravity, State);
        }
    }

}