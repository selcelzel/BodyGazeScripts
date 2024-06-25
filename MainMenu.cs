using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject gender1Character;
    public GameObject gender2Character;
    public Button gender1Button;
    public Button gender2Button;
    public GameObject menuwriting2;
    public GameObject menuwriting3;
    public Button startButton;
    //public Button calibrationButton;
    public Button quitButton;
    public GameObject menu;
    public GameObject canvas;

    void Start()
    {
        // Attach functions to buttons
        gender1Button.onClick.AddListener(SelectGender1);
        gender2Button.onClick.AddListener(SelectGender2);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);

        menuwriting3.SetActive(false);

        // Hide unnecessary buttons and characters initially
        gender1Character.SetActive(false);
        gender2Character.SetActive(false);

        // Set start and quit buttons to be inactive initially
        startButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }

    void StartGame()
    {
        // Check which gender is selected
        if (gender1Character.activeSelf)
        {
            // Implement logic for starting the game with Gender 1
            Debug.Log("Starting the game with Gender 1!");
        }
        else if (gender2Character.activeSelf)
        {
            // Implement logic for starting the game with Gender 2
            Debug.Log("Starting the game with Gender 2!");
        }
        else
        {
            // Handle the case where no gender is selected
            Debug.LogWarning("No gender selected!");
        }

        // Hide the entire menu
        HideMenu();
    }

    void SelectGender1()
    {
        gender1Character.SetActive(true);
        gender2Character.SetActive(false);

        // Set start and quit buttons to be active
        startButton.gameObject.SetActive(true);
        //calibrationButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
        menuwriting3.gameObject.SetActive(true);


        // Hide other UI elements
        gender1Button.gameObject.SetActive(false);
        gender2Button.gameObject.SetActive(false);
        menuwriting2.gameObject.SetActive(false);
    }

    void SelectGender2()
    {
        gender2Character.SetActive(true);
        gender1Character.SetActive(false);

        // Set start and quit buttons to be active
        startButton.gameObject.SetActive(true);
        //calibrationButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
        menuwriting3.gameObject.SetActive(true);

        // Hide other UI elements
        gender1Button.gameObject.SetActive(false);
        gender2Button.gameObject.SetActive(false);
        menuwriting2.gameObject.SetActive(false);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void HideMenu()
    {
        menu.gameObject.SetActive(false);
    }
}