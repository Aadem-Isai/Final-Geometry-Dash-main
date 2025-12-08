using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text statusText;

    public void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }
}
