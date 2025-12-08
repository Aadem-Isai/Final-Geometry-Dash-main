using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public void SetColor(string colorName)
    {
        PlayerColorSettings.SetColor(colorName);
    }
}