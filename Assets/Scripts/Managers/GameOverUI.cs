using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsScriptableObject playerStats;

    public void Restart()
    {
        playerStats.ResetHealth();
        playerStats.ResetScore();
        SceneManager.LoadScene("Level01");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
