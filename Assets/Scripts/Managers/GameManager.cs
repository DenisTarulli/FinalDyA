using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject playerUI;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private SceneFader sceneFader;

    [SerializeField] private string nextLevelScene;
    [SerializeField] private Transform collectibles;
    private int totalCollectibles;

    [SerializeField] private PlayerStatsScriptableObject playerStats;

    public bool gameIsOver;
    private bool levelOver;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;

        totalCollectibles = collectibles.childCount;
        UpdateScoreText(playerStats.score);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();
    }

    private void RestartLevel()
    {
        playerStats.ResetHealth();
        playerStats.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = $"SCORE: {score:D5}";
    }

    public void DecreaseCollectibleAmount()
    {
        totalCollectibles--;

        if (totalCollectibles <= 0)
            NextLevel(nextLevelScene);
    }

    private void NextLevel(string level)
    {
        levelOver = true;
        sceneFader.FadeTo(level);
    }

    public void GameOver()
    {
        if (levelOver) return;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        playerUI.SetActive(false);

        gameIsOver = true;

        gameOverUI.SetActive(true);
    }

    private void OnEnable()
    {
        playerStats.OnScoreUpdate.AddListener(UpdateScoreText);
    }

    private void OnDisable()
    {
        playerStats.OnScoreUpdate.RemoveListener(UpdateScoreText);
    }
}
