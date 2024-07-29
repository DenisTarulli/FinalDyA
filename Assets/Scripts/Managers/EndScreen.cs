using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private PlayerStatsScriptableObject playerStats;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        scoreText.text = $"FINAL SCORE: {playerStats.score:D5}";
    }

    public void Restart()
    {
        playerStats.ResetHealth();
        playerStats.ResetScore();
        sceneFader.FadeTo("Level01");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
