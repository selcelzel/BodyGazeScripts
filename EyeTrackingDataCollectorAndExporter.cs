using UnityEngine;
using System.IO;
using ViveSR.anipal.Eye;
using System;
using System.Globalization;

public class EyeTrackingDataCollectorAndExporter : MonoBehaviour
{
    private bool isTracking = false;
    private float timer = 0f;
    private float timerSinceLastCapture = 0f;
    private const float sampleInterval = 0.009f;

    // File path to save the exported data
    public string filePath;

    void Start()
    {
        // Set file path to the desktop
        filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/eye_tracking_data.txt";

        StartEyeTracking();

        timer = 0f;
        timerSinceLastCapture = 0f; // Initialize timer since last capture

    }

    void Update()
    {
        if (isTracking)
        {
            // Update the timer
            timer += Time.deltaTime;

            timerSinceLastCapture += Time.deltaTime;

            if (timerSinceLastCapture >= sampleInterval)
            {
                Vector3 leftGazeDirection;
                Vector3 rightGazeDirection;
                bool eyeDataValid = SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out leftGazeDirection, out rightGazeDirection);

                if (eyeDataValid)
                {
                    // Process eye tracking data 
                    Vector3 combinedGazeDirection = (leftGazeDirection + rightGazeDirection) * 0.5f; // Combined gaze direction

                    Debug.Log("Combined Gaze Direction: " + combinedGazeDirection.ToString("F4"));

                    WriteToTextFile(combinedGazeDirection);

                    timerSinceLastCapture = 0f;
                }
            }
        }
    }

    void OnDestroy()
    {
        StopEyeTracking();
    }

    void StartEyeTracking()
    {
        isTracking = true;
        SRanipal_Eye_Framework.Instance.EnableEye = true;
    }

    void StopEyeTracking()
    {
        SRanipal_Eye_Framework.Instance.EnableEye = false;
    }


    void WriteToTextFile(Vector3 gazeDirection)
    {
        bool fileExists = File.Exists(filePath);
        CultureInfo culture = CultureInfo.InvariantCulture;
        StreamWriter writer = new StreamWriter(filePath, true);

        if (!fileExists)
        {
            writer.WriteLine("X Pos\tX Pix\tY Pos\tY Pix\tTimer");
        }

        // Write eye tracking data to the text file
        writer.WriteLine(gazeDirection.x.ToString("F4", culture) + "\t" + ConvertToPixelCoordinates(gazeDirection.x)
                             + "\t" + gazeDirection.y.ToString("F4", culture) + "\t" + ConvertToPixelCoordinates(gazeDirection.y)
                             + "\t" + timer.ToString("F4", culture));

        writer.Close();
    }

    int ConvertToPixelCoordinates(float coordinate)
    {
        return Mathf.RoundToInt(coordinate * Screen.width);
    }
}
