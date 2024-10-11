using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0; // Player's current score
    public int highScore = 0; // The high score

    public TextMeshProUGUI scoreText; // UI Text element to display the score
    public TextMeshProUGUI highScoreText; // UI Text element to display the high score

    private void Awake()
    {
        // Ensure there is only one instance of the ScoreManager (singleton pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the high score from saved data
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    // Call this method to increase the score when an enemy is destroyed
    public void AddScore(int points)
    {
        currentScore += points;

        // Check if the new score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            // Save the new high score
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateScoreUI();
    }

    // Update the score UI elements
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    // Reset score (if needed)
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }
}
