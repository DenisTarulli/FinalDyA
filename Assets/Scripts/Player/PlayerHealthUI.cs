using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private Slider playerHealthBar;
    private PlayerCombat player;

    [SerializeField] private PlayerStatsScriptableObject playerStats;    

    private void Start()
    {
        player = FindObjectOfType<PlayerCombat>();
        playerHealthBar = GetComponent<Slider>();
        SetMaxHealth();
    }

    private void SetMaxHealth()
    {
        playerHealthBar.maxValue = playerStats.maxHealth;
        playerHealthBar.value = playerStats.currentHealth;
    }

    public void SetHealth(int health)
    {
        playerHealthBar.value = health;
    }

    private void OnEnable()
    {
        playerStats.OnHealthUpdate.AddListener(SetHealth);
    }

    private void OnDisable()
    {
        playerStats.OnHealthUpdate.RemoveListener(SetHealth);
    }
}
