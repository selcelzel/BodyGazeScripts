using UnityEngine;
using UnityEngine.UI;

public class TeleportObject : MonoBehaviour
{
    // Reference to the panel or area where the object can teleport
    public RectTransform panelRect;

    // Reference to the slider to display teleport count
    public Slider teleportCountSlider;

    // Total number of times the object will teleport
    public int totalTeleports = 34;

    // Number of times the object has teleported
    public int teleportCount = 0;

    // Reference to the VRGameController script
    public VRGameController gameController;

    // Method to handle object selection
    public void OnObjectSelected()
    {
        // Teleport the object to a random position inside the panel
        TeleportToRandomPosition();

        // Increment teleport count
        teleportCount++;

        // Update the slider value
        if (teleportCountSlider != null)
        {
            teleportCountSlider.value = teleportCount;
        }
    }

    // Method to teleport the object to a random position inside the panel
    private void TeleportToRandomPosition()
    {
        // Ensure the panelRect is assigned
        if (panelRect == null)
        {
            Debug.LogError("Panel reference is not assigned!");
            return;
        }

        // Calculate boundaries of the panel
        Vector3 panelMin = panelRect.TransformPoint(panelRect.rect.min);
        Vector3 panelMax = panelRect.TransformPoint(panelRect.rect.max);

        // Generate random position within panel boundaries
        float randomX = Random.Range(panelMin.x, panelMax.x);
        float randomY = Random.Range(panelMin.y, panelMax.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, transform.position.z);

        // Move the object to the random position
        transform.position = randomPosition;
    }

    // Initialize the slider's maximum value
    private void Start()
    {
        if (teleportCountSlider != null)
        {
            teleportCountSlider.maxValue = totalTeleports;
        }
    }
}
