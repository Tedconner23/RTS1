using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // Game variables
    public static int playerScore = 0;
    public static int playerLives = 3;

    // Reference to the start button
    public Button startButton;

    void Start()
    {
        // Initialize game variables
        playerScore = 0;
        playerLives = 3;

        // Add listener to the start button
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
    }

    void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }
}
