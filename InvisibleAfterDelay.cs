using UnityEngine;

public class InvisibleAfterDelay : MonoBehaviour
{
    private float delay = 5f; // Time in seconds before the object becomes invisible
    private float timer = 0f;
    private bool isVisible = true;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer component not found on the GameObject. This script requires a Renderer to work properly.");
            enabled = false; // Disable the script if Renderer is not found
        }
    }

    void Update()
    {
        if (isVisible)
        {
            timer += Time.deltaTime;

            if (timer >= delay)
            {
                // Make the object invisible
                objectRenderer.enabled = false;
                isVisible = false;
            }
        }
    }
}
