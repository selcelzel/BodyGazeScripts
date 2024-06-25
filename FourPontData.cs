using UnityEngine;
using System.IO;
using ViveSR.anipal.Eye;
using System;
using System.Globalization;

public class FourPontData : MonoBehaviour
{
    private bool isTracking = false;
    private float timer = 0f;
    private float timerSinceLastCapture = 0f; // New variable to track time since last capture
    private const float sampleInterval = 0.009f; // Sample interval in seconds (9 milliseconds)

    // File path to save the exported data
    public string filePath;

    void Start()
    {
        // Set file path to the desktop
        filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/four_point_data.txt";

        // Start eye tracking
        StartEyeTracking();

        // Start timer
        timer = 0f;
        timerSinceLastCapture = 0f; // Initialize timer since last capture

    }

    void Update()
    {
        if (isTracking)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Update the timer since last capture
            timerSinceLastCapture += Time.deltaTime;

            // Check if it's time to capture a new sample
            if (timerSinceLastCapture >= sampleInterval)
            {
                Vector3 leftGazeDirection;
                Vector3 rightGazeDirection;
                bool eyeDataValid = SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out leftGazeDirection, out rightGazeDirection);

                if (eyeDataValid)
                {
                    // Process eye tracking data here
                    Vector3 combinedGazeDirection = (leftGazeDirection + rightGazeDirection) * 0.5f; // Combined gaze direction

                    // Log the data to console
                    Debug.Log("Combined Gaze Direction: " + combinedGazeDirection.ToString("F4"));

                    // Export data to text file
                    WriteToTextFile(combinedGazeDirection);

                    // Reset the timer since last capture
                    timerSinceLastCapture = 0f;
                }
            }
        }
    }

    void OnDestroy()
    {
        // Clean up
        StopEyeTracking();
    }

    void StartEyeTracking()
    {
        // Start eye tracking
        isTracking = true;
        SRanipal_Eye_Framework.Instance.EnableEye = true;
    }

    void StopEyeTracking()
    {
        // Stop eye tracking
        SRanipal_Eye_Framework.Instance.EnableEye = false;
    }


    void WriteToTextFile(Vector3 gazeDirection)
    {
        // Check if the file exists
        bool fileExists = File.Exists(filePath);

        // Set the culture info to use period as the decimal separator
        CultureInfo culture = CultureInfo.InvariantCulture;

        // Open or create a text file for writing
        StreamWriter writer = new StreamWriter(filePath, true);

        // Write headers to the text file only if it's the first time
        if (!fileExists)
        {
            writer.WriteLine("X Pos\tX Pix\tY Pos\tY Pix\tTimer");
        }

        // Write eye tracking data to the text file
        writer.WriteLine(gazeDirection.x.ToString("F4", culture) + "\t" + ConvertToPixelCoordinates(gazeDirection.x)
                             + "\t" + gazeDirection.y.ToString("F4", culture) + "\t" + ConvertToPixelCoordinates(gazeDirection.y)
                             + "\t" + timer.ToString("F4", culture));

        // Close the file
        writer.Close();
    }

    // Helper method to convert coordinate to pixel coordinates
    int ConvertToPixelCoordinates(float coordinate)
    {
        // Assuming coordinate represents normalized coordinates, convert them to pixel coordinates
        return Mathf.RoundToInt(coordinate * Screen.width);
    }
}