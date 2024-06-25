using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRGameController : MonoBehaviour
{
    public GameObject[] items;
    /*public GameObject rightShoulder;
    public GameObject leftShoulder;
    public GameObject rightLeg;
    public GameObject leftLeg;*/
    public Slider progressBar;
    public GameObject[] congratsScreens;
    public Slider[] congratsSliders; 
    public GameObject[] continueButtons;
    public Button startButton;
    public GameObject[] objectsToHideDuringCongrats; // Array of game objects to hide during congrats screens
    
    private int currentItemIndex = 0;
    private float totalTime = 30f;
    private float congratsScreenTimer = 1f; // Timer for each congrats screen
    private bool congratsScreenVisible = false;
    private bool gameStarted = false;

    public GameObject canvas1;
    public GameObject canvas2;

    public TeleportObject teleportObject1;
    public TeleportObject teleportObject2;
    public TeleportObject teleportObject3;
    public TeleportObject teleportObject4;

    void Start()
    {
        startButton.onClick.AddListener(StartButtonClicked);
        HideAllItems();
        HideAllCongratsScreens();
        HideAllContinueButtons();
        HideObjectsDuringCongrats();
        HideCanvases();
        HideObjects();

        for (int i = 0; i < continueButtons.Length; i++)
        {
            int index = i;
            continueButtons[index].GetComponent<Button>().onClick.AddListener(() => ContinueButtonPressed(index));
        }
        
    }

    void StartButtonClicked()
    {
        gameStarted = false;
        currentItemIndex = 0;
        HideAllItems();
        HideAllCongratsScreens();
        HideAllContinueButtons();
        HideObjectsDuringCongrats();
        HideCanvases();
        gameStarted = true;
        SetActiveItem(0);
        congratsScreenTimer = 1f;
        HideAllContinueButtons();
    }

    void SetActiveItem(int index)
    {
        if (index > 0)
        {
            items[index - 1].SetActive(false);
        }

        items[index].SetActive(true);

        ModifyCanvasVisibility(index);

        if ((index == 1) && teleportObject1 != null)
        {
            // Activate TeleportObject1
            teleportObject1.gameObject.SetActive(true);
        }
        else
        {
            // Deactivate TeleportObject1 if not in use
            if (teleportObject1 != null)
            {
                teleportObject1.gameObject.SetActive(false);
            }
        }

        if ((index == 2) && teleportObject2 != null)
        {
            // Activate TeleportObject2
            teleportObject2.gameObject.SetActive(true);
        }
        else
        {
            // Deactivate TeleportObject2 if not in use
            if (teleportObject2 != null)
            {
                teleportObject2.gameObject.SetActive(false);
            }
        }

        if ((index == 3) && teleportObject3 != null)
        {
            teleportObject3.gameObject.SetActive(true);
        }
        else
        {
            if (teleportObject3 != null)
            {
                teleportObject3.gameObject.SetActive(false);
            }
        }

        if ((index == 4) && teleportObject4 != null)
        {
            teleportObject4.gameObject.SetActive(true);
        }
        else
        {
            if (teleportObject4 != null)
            {
                teleportObject4.gameObject.SetActive(false);
            }
        }


        if (index >= 1 && index <= 4)
        {
            // Deactivate VRGameController's slider
            progressBar.gameObject.SetActive(false);
            // Activate TeleportObject's slider
            if (teleportObject1 != null && teleportObject1.teleportCountSlider != null)
            {
                teleportObject1.teleportCountSlider.gameObject.SetActive(true);
            }
            if (teleportObject2 != null && teleportObject2.teleportCountSlider != null)
            {
                teleportObject2.teleportCountSlider.gameObject.SetActive(true);
            }
            if (teleportObject3 != null && teleportObject3.teleportCountSlider != null)
            {
                teleportObject3.teleportCountSlider.gameObject.SetActive(true);
            }
            if (teleportObject4 != null && teleportObject4.teleportCountSlider != null)
            {
                teleportObject4.teleportCountSlider.gameObject.SetActive(true);
            }
        }
        else
        {
            // Activate VRGameController's slider
            progressBar.gameObject.SetActive(true);
            // Deactivate TeleportObject's slider
            if (teleportObject1 != null && teleportObject1.teleportCountSlider != null)
            {
                teleportObject1.teleportCountSlider.gameObject.SetActive(false);
            }
            if (teleportObject2 != null && teleportObject2.teleportCountSlider != null)
            {
                teleportObject2.teleportCountSlider.gameObject.SetActive(false);
            }
            if (teleportObject3 != null && teleportObject3.teleportCountSlider != null)
            {
                teleportObject3.teleportCountSlider.gameObject.SetActive(false);
            }
            if (teleportObject4 != null && teleportObject4.teleportCountSlider != null)
            {
                teleportObject4.teleportCountSlider.gameObject.SetActive(false);
            }
        }

        //ModifyImageVisibility(index);

        //ModifyAvatarBodyParts(index);
    }

    public void UpdateProgressBar(float progress)
    {
        progressBar.value = progress;
    }

    public void ShowCongratsScreen(int index)
    {
        // Hide all items before showing congrats screen
        HideAllItems();

        congratsScreens[index].SetActive(true);
        continueButtons[index].SetActive(true);
        congratsSliders[index].value = 0.0f; // Reset the slider value
        congratsSliders[index].gameObject.SetActive(true); // Activate the slider
        congratsScreenVisible = true;
        congratsScreenTimer = 1f; // Reset the timer when showing a new congrats screen

        if (teleportObject1 != null && teleportObject1.teleportCountSlider != null)
        {
            teleportObject1.teleportCountSlider.value = 0.0f;
        }

        if (teleportObject2 != null && teleportObject2.teleportCountSlider != null)
        {
            teleportObject2.teleportCountSlider.value = 0.0f;
        }

        if (teleportObject3 != null && teleportObject3.teleportCountSlider != null)
        {
            teleportObject3.teleportCountSlider.value = 0.0f;
        }

        if (teleportObject4 != null && teleportObject4.teleportCountSlider != null)
        {
            teleportObject4.teleportCountSlider.value = 0.0f;
        }

        // Hide specified objects during congrats screen
        foreach (var obj in objectsToHideDuringCongrats)
        {
            obj.SetActive(false);
        }
    }

    void HideObjectsDuringCongrats()
    {
        // Show specified objects when congrats screen is not visible
        foreach (var obj in objectsToHideDuringCongrats)
        {
            obj.SetActive(true);
        }
    }

    void HideAllItems()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }
    }

    void HideAllCongratsScreens()
    {
        foreach (var screen in congratsScreens)
        {
            screen.SetActive(false);
        }

        congratsScreenVisible = false;
    }

    void HideAllContinueButtons()
    {
        foreach (var button in continueButtons)
        {
            button.SetActive(false);
        }
    }

    public void ContinueButtonPressed(int index)
    {
        if (congratsScreenVisible)
        {
            congratsScreens[index].SetActive(false);
            continueButtons[currentItemIndex].SetActive(false);
            congratsSliders[index].gameObject.SetActive(false);
            congratsScreenVisible = false;

            // Reset the progress bar
            progressBar.value = 0.0f;

            currentItemIndex++;

            if (currentItemIndex >= items.Length)
            {
                //Debug.Log("Game Completed!");
                gameStarted = false;
                currentItemIndex = 0;
            }
            else
            {
                SetActiveItem(currentItemIndex);
                gameStarted = true;
            }

            // Show specified objects after hiding during congrats screen
            HideObjectsDuringCongrats();
        }
    }

    void Update()
    {
        if (gameStarted)
        {
            float progressSpeed = 1f / totalTime;
            float progress = Mathf.Clamp01(progressBar.value + Time.deltaTime * progressSpeed);

            UpdateProgressBar(progress);

            if (congratsScreenVisible)
            {
                congratsSliders[currentItemIndex].value = Mathf.Clamp01(congratsSliders[currentItemIndex].value + Time.deltaTime / congratsScreenTimer);

                congratsScreenTimer -= Time.deltaTime;

                if (congratsScreenTimer <= 0)
                {
                    continueButtons[currentItemIndex].SetActive(true);
                }
            }

            if ((currentItemIndex == 0 || currentItemIndex == 5) && progressBar.value < 1.0f) 
            {
                totalTime = 5f; 
            }
            else
            {
                totalTime = 1000f;
            }

            if (progressBar.value >= 1.0f && !congratsScreenVisible)
            {
                ShowCongratsScreen(currentItemIndex);
            }

            if (congratsScreenVisible && Input.GetButtonDown("Fire1"))
            {
                ContinueButtonPressed(currentItemIndex);
            }

            if ((currentItemIndex == 1) && teleportObject1 != null && !congratsScreenVisible)
            {
                // If yes, update the teleport counter slider value
                if (teleportObject1.teleportCountSlider != null)
                {
                    teleportObject1.teleportCountSlider.value = teleportObject1.teleportCount;
                }

                // Check if the teleport count reaches the total number of teleports
                if (teleportObject1.teleportCount >= teleportObject1.totalTeleports)
                {
                    // Show congrats screen when the teleport count reaches the total
                    ShowCongratsScreen(currentItemIndex);
                }
            }
            else if ((currentItemIndex == 2) && teleportObject2 != null && !congratsScreenVisible)
            {
                // If yes, update the teleport counter slider value
                if (teleportObject2.teleportCountSlider != null)
                {
                    teleportObject2.teleportCountSlider.value = teleportObject2.teleportCount;
                }

                // Check if the teleport count reaches the total number of teleports
                if (teleportObject2.teleportCount >= teleportObject2.totalTeleports)
                {
                    // Show congrats screen when the teleport count reaches the total
                    ShowCongratsScreen(currentItemIndex);
                }
            }
            else if ((currentItemIndex == 3) && teleportObject3 != null && !congratsScreenVisible)
            {
                // If yes, update the teleport counter slider value
                if (teleportObject3.teleportCountSlider != null)
                {
                    teleportObject3.teleportCountSlider.value = teleportObject3.teleportCount;
                }

                // Check if the teleport count reaches the total number of teleports
                if (teleportObject3.teleportCount >= teleportObject3.totalTeleports)
                {
                    // Show congrats screen when the teleport count reaches the total
                    ShowCongratsScreen(currentItemIndex);
                }
            }
            else if ((currentItemIndex == 4) && teleportObject4 != null && !congratsScreenVisible)
            {
                // If yes, update the teleport counter slider value
                if (teleportObject4.teleportCountSlider != null)
                {
                    teleportObject4.teleportCountSlider.value = teleportObject4.teleportCount;
                }

                // Check if the teleport count reaches the total number of teleports
                if (teleportObject4.teleportCount >= teleportObject4.totalTeleports)
                {
                    // Show congrats screen when the teleport count reaches the total
                    ShowCongratsScreen(currentItemIndex);
                }
            }

        }
    }

    void HideCanvases()
    {
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
    }

    void HideObjects()
    {
        teleportObject1.gameObject.SetActive(false);
        teleportObject2.gameObject.SetActive(false);
        teleportObject3.gameObject.SetActive(false);
        teleportObject4.gameObject.SetActive(false);
    }

    void SetCanvasVisibility(int index)
    {

        // Toggle visibility based on the current item index
        if (index % 2 == 0)
        {
            canvas1.SetActive(true);
            canvas2.SetActive(false);
        }
        else
        {
            canvas1.SetActive(false);
            canvas2.SetActive(true);
        }
    }

    void ModifyCanvasVisibility(int index)
    {
        switch (index)
        {
            case 0: // Element 0
                canvas1.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(false);
                break;
            case 1: // Element 1
                canvas1.gameObject.SetActive(true);
                canvas2.gameObject.SetActive(false);
                break;
            case 2: // Element 2
                canvas1.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(true);
                break;
            case 3: // Element 3
                canvas1.gameObject.SetActive(true);
                canvas2.gameObject.SetActive(false);
                break;
            case 4: // Element 4
                canvas1.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(true);
                break;
            case 5: // Element 5
                canvas1.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(false);
                break;
                // Add more cases for additional items if needed
        }
    }
    

    /*public void ResetGame()
    {
        gameStarted = false;
        currentItemIndex = 0;

        // Hide all items, congrats screens, and continue buttons
        HideAllItems();
        HideAllCongratsScreens();
        HideAllContinueButtons();

        // Reset progress bar
        progressBar.value = 0.0f;
    }*/

    /*void ModifyImageVisibility(int index)
    {
        switch (index)
        {
            case 0: // Element 0
                imageToHide.gameObject.SetActive(false);
                break;
            case 1: // Element 1
                // Additional logic if needed
                break;
            case 2: // Element 2
                // Additional logic if needed
                break;
            case 3: // Element 3
                // Additional logic if needed
                break;
            case 4: // Element 4
                // Additional logic if needed
                break;
            case 5: // Element 5
                imageToHide.gameObject.SetActive(false);
                break;
                // Add more cases for additional items if needed
        }
    }

    void HideImage()
    {
        imageToHide.gameObject.SetActive(false);
    }*/


    /*void ModifyAvatarBodyParts(int index)
    {
        // Reset all body parts to normal size
        rightShoulder.transform.localScale = Vector3.one;
        leftShoulder.transform.localScale = Vector3.one;
        rightLeg.transform.localScale = Vector3.one;
        leftLeg.transform.localScale = Vector3.one;

        // Modify body parts based on the current item index
        switch (index)
        {
            case 0: // Right Shoulder
                rightShoulder.transform.localScale *= 1.5f;
                break;
            case 1: // Left Shoulder
                leftShoulder.transform.localScale *= 1.5f;
                break;
            case 2: // Right Leg
                // Only change the x-axis scale
                rightLeg.transform.localScale = new Vector3(2f, rightLeg.transform.localScale.y, rightLeg.transform.localScale.z);
                break;
            case 3: // Left Leg
                // Only change the x-axis scale
                leftLeg.transform.localScale = new Vector3(2f, leftLeg.transform.localScale.y, leftLeg.transform.localScale.z);
                break;
                // Add more cases for additional items if needed
        }
    }*/


}