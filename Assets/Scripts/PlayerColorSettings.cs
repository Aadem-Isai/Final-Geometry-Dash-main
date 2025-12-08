using UnityEngine;

public static class PlayerColorSettings
{
    public static Color selectedColor = Color.white;
    
    public static void SetColor(string colorName)
    {
        switch(colorName.ToLower())
        {
            case "red":
                selectedColor = Color.red;
                break;
            case "blue":
                selectedColor = Color.blue;
                break;
            case "green":
                selectedColor = Color.green;
                break;
            case "yellow":
                selectedColor = Color.yellow;
                break;
            case "purple":
                selectedColor = new Color(0.5f, 0f, 0.5f);
                break;
            case "white":
                selectedColor = Color.white;
                break;
            case "black":
                selectedColor = Color.black;
                break;
            case "orange":
                selectedColor = new Color(1f, 0.5f, 0f);
                break;
        }
        
        Debug.Log("Player color set to: " + colorName);
    }
}