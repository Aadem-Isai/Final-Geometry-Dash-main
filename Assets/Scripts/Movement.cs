using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum Speeds { Slow = 0, Medium = 1, Fast = 2, Faster = 3, Fastest = 4 };
public enum GameModes { Cube = 0, Ship = 1 };
public enum Gravity { Normal = 1, Inverted = -1 };

public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed = 0;
    public GameModes CurrentGameMode = GameModes.Cube;
    float[] SpeedValues = { 0f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform GroundCheckTransform;
    public float GroundCheckRadius = 0.1f;
    public LayerMask GroundMask;
    public Transform Sprite;
    public Text startText;

    public GameObject cubeSprite;
    public GameObject shipSprite;
    public float shipFlySpeed = 20f;

    Rigidbody2D rb;
    private bool gameStarted = false;
    private bool hasJumped = false;
    private bool spacePressed = false;
    public bool isGravityInverted = false;  // Track gravity state

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 12.4f;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        if (startText != null)
            startText.gameObject.SetActive(true);
            
        SetGameMode(GameModes.Cube);
        
        ApplyPlayerColor();
    }

    void ApplyPlayerColor()
    {
        Color playerColor = PlayerColorSettings.selectedColor;
        Debug.Log("=== APPLYING COLOR ===");
        Debug.Log("PlayerColorSettings.selectedColor = " + playerColor);
        
        // Apply to cube sprite
        if (cubeSprite != null)
        {
            Debug.Log("cubeSprite exists");
            SpriteRenderer cubeSR = cubeSprite.GetComponent<SpriteRenderer>();
            if (cubeSR != null)
            {
                cubeSR.color = playerColor;
                Debug.Log("Cube color NOW: " + cubeSR.color);
            }
            else Debug.Log("ERROR: cubeSprite has no SpriteRenderer!");
        }
        else Debug.Log("ERROR: cubeSprite is NULL!");
        
        // Apply to ship sprite
        if (shipSprite != null)
        {
            Debug.Log("shipSprite exists");
            SpriteRenderer shipSR = shipSprite.GetComponent<SpriteRenderer>();
            if (shipSR != null)
            {
                shipSR.color = playerColor;
                Debug.Log("Ship color NOW: " + shipSR.color);
            }
            else Debug.Log("ERROR: shipSprite has no SpriteRenderer!");
        }
        else Debug.Log("ERROR: shipSprite is NULL!");
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                gameStarted = true;
                CurrentSpeed = Speeds.Medium;
                if (startText != null)
                    startText.gameObject.SetActive(false);
            }
            return;
        }

        spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
        UpdateVisuals();
    }

    void FixedUpdate()
    {
        if (!gameStarted) return;

        float currentYVelocity = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(SpeedValues[(int)CurrentSpeed], currentYVelocity);

        if (CurrentGameMode == GameModes.Cube)
        {
            UpdateCubeModePhysics();
        }
        else if (CurrentGameMode == GameModes.Ship)
        {
            UpdateShipModePhysics();
        }
    }

    void UpdateCubeModePhysics()
    {
        if (OnGround())
        {
            hasJumped = false;

            if (spacePressed && !hasJumped)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                
                float jumpDirection = Mathf.Sign(rb.gravityScale);
                rb.AddForce(Vector2.up * jumpDirection * 26.6581f, ForceMode2D.Impulse);
                
                hasJumped = true;
            }
        }
    }

    void UpdateShipModePhysics()
    {
        // Ship respects gravity inversion!
        float targetVelocityY;
        
        if (isGravityInverted)
        {
            // Upside down ship: space = fly DOWN, release = fly UP
            targetVelocityY = spacePressed ? -shipFlySpeed : shipFlySpeed;
        }
        else
        {
            // Normal ship: space = fly UP, release = fly DOWN
            targetVelocityY = spacePressed ? shipFlySpeed : -shipFlySpeed;
        }
        
        float smoothVelocityY = Mathf.Lerp(rb.linearVelocity.y, targetVelocityY, Time.fixedDeltaTime * 10f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, smoothVelocityY);
    }

    void UpdateVisuals()
        {
            if (Sprite == null) return;

            if (CurrentGameMode == GameModes.Cube)
            {
                if (OnGround())
                {
                    // STOP rotation when on ground (including slopes)
                    Vector3 Rotation = Sprite.rotation.eulerAngles;
                    Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
                    Sprite.rotation = Quaternion.Euler(Rotation);
                    
                    // Keep it locked at 0 or 90 degree increments
                    Sprite.rotation = Quaternion.Euler(0, 0, 0);  // Force to 0 degrees
                }
                else
                {
                    // Only rotate in mid-air
                    Sprite.Rotate(Vector3.back * 2);
                }
            }
            else if (CurrentGameMode == GameModes.Ship)
            {
                float baseTilt = rb.linearVelocity.y > 0 ? -15f : 15f;
                if (isGravityInverted) baseTilt *= -1;
                
                Sprite.rotation = Quaternion.Lerp(Sprite.rotation, Quaternion.Euler(0, 0, baseTilt), Time.deltaTime * 8f);
            }
        }


    bool OnGround()
    {
        if (GroundCheckTransform == null)
            return false;

        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }

    void SetGameMode(GameModes newMode)
        {
            CurrentGameMode = newMode;
            
            if (newMode == GameModes.Cube)
            {
                if (cubeSprite != null) cubeSprite.SetActive(true);
                if (shipSprite != null) shipSprite.SetActive(false);
                
                // Restore gravity with correct direction
                rb.gravityScale = isGravityInverted ? -12.4f : 12.4f;
            }
            else if (newMode == GameModes.Ship)
            {
                if (cubeSprite != null) cubeSprite.SetActive(false);
                if (shipSprite != null) shipSprite.SetActive(true);
                
                // BEFORE setting gravity to 0, check if it was inverted
                if (rb.gravityScale < 0)
                {
                    isGravityInverted = true;
                }
                else if (rb.gravityScale > 0)
                {
                    isGravityInverted = false;
                }
                
                // Ship doesn't use gravity, but remembers the state
                rb.gravityScale = 0f;
                
                Debug.Log("Switched to ship. Gravity inverted: " + isGravityInverted);
            }
        }

    public void ChangeThroughPortal(GameModes Gamemode, Speeds Speed, Gravity gravity, int State)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        switch(State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                SetGameMode(Gamemode);
                Debug.Log("Mode changed to: " + Gamemode + " | Gravity inverted: " + isGravityInverted);
                break;
            case 2:
                // Update gravity state for BOTH cube and ship
                isGravityInverted = ((int)gravity == -1);
                
                // Update actual gravity scale
                if (CurrentGameMode == GameModes.Cube)
                {
                    rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
                }
                else if (CurrentGameMode == GameModes.Ship)
                {
                    // Ship gravity stays 0, but state is tracked for controls
                    rb.gravityScale = 0f;
                }
                
                Debug.Log("Gravity changed. Inverted: " + isGravityInverted + " | Mode: " + CurrentGameMode);
                break;
        }
    }
}