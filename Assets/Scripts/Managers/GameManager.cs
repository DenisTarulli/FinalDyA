using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    private PlayerCombat player;
    [SerializeField] private string nextLevelScene;
    [SerializeField] private Transform collectibles;
    private int totalCollectibles;

    public bool gameIsOver;

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

        player = FindObjectOfType<PlayerCombat>();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"SCORE: {score:D6}";

        totalCollectibles--;

        if (totalCollectibles <= 0)
            NextLevel(nextLevelScene);

    }

    private void NextLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        gameIsOver = true;

        gameOverUI.SetActive(true);

        if (player.CurrentHealth > 0)
            winText.SetActive(true);
        else
            loseText.SetActive(true);
    }

    private void OnEnable()
    {
        Item.OnItemCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Item.OnItemCollected -= UpdateScore;
    }
}
