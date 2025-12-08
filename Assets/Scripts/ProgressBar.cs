using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform levelStart;
    public Transform levelEnd;
    public Slider slider;

    void Update()
    {
        if (player == null || levelStart == null || levelEnd == null || slider == null)
            return;

        float startX = levelStart.position.x;
        float endX = levelEnd.position.x;
        float currentX = Mathf.Clamp(player.position.x, startX, endX);

        float progress = Mathf.InverseLerp(startX, endX, currentX);
        slider.value = progress;
    }
}
