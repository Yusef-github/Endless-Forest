using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager class manages game-related functionality, such as scoring, pausing, and game over handling.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Score
    private float score; //Add [SerializeField] for testing only. 
    [SerializeField] private TextMeshProUGUI scoreText;

    // HighScore
    private float highScore = 0; //Add [SerializeField] for testing only. 
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Singleton instance, providing global access to this script functionality.
    public static GameManager gameManager;

    // UI Elements to Activate/Deactivate.
    [SerializeField] private GameObject StartPanel;

    private void Awake()
    {
        Time.timeScale = 0;

        gameManager = this; // ensures that there is only one GameManager throughout the game.

        highScore = PlayerPrefs.GetFloat("HighScore", 0);// Load the high score from PlayerPrefs.
        UpdateHighScoreText();
    }

    // Updates the high score displayed in the UI.
    private void UpdateHighScoreText() => highScoreText.text = "Best\n" + Mathf.Floor(highScore);


    // After pressing "Play Button", the time scale set to 1 and hiding the start panel.
    public void Play()
    {
        Time.timeScale = 1;
        PlayerMovement.playerMovement.alive = true;
        PlayerMovement.playerMovement.FellDown = true;
        PlayerMovement.playerMovement.CharacterAnimator.enabled = true;
        StartPanel.SetActive(false);
    }


    // Pauses the game by setting the time scale to 0.
    public void Pause() => Time.timeScale = 0;

    // Resumes the game by setting the time scale to 1.
    public void Resume() => Time.timeScale = 1;


    // Restarts the current scene.
    public void Restart() => SceneManager.LoadScene("SampleScene");

    private void Update()
    {
        if (PlayerMovement.playerMovement.alive == true)
        {
            score += PlayerMovement.playerMovement.speed * 0.01f;
            scoreText.text = " Points\n" + Mathf.Floor(score);

            if (score > highScore)
            {
                highScore = score;

                PlayerPrefs.SetFloat("HighScore", highScore);
                PlayerPrefs.Save();

                UpdateHighScoreText();
            }
        }
    }

    // Called when the player collides with a coin. Adds 10 points to the player's score.
    public void CollideWithCoin() => score += 10;
}