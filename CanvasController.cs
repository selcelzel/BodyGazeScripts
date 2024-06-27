using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas myCanvas;
    private float delay = 30f;
    private float timer = 0f;

    void Update()
    {
        // Increment the timer each frame
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            // Set the Canvas to be visible
            myCanvas.enabled = true;
        }
    }
}

