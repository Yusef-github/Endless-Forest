using UnityEngine;
public class HighScoreManager : MonoBehaviour
{
    private int highScore;

    // Load the high score from PlayerPrefs when the game starts
    void Start() => LoadHighScore();

    // Save the high score to PlayerPrefs
    public void SaveHighScore(int newHighScore)
    {
        if (newHighScore > highScore)
        {
            highScore = newHighScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    // Load the high score from PlayerPrefs
    void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetInt("HighScore");
    }

    // Get the current high score
    public int GetHighScore()
    {
        return highScore;
    }
}
