using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private Slider playerHealthBar;
    private PlayerCombat player;

    private void Start()
    {
        player = FindObjectOfType<PlayerCombat>();
        playerHealthBar = GetComponent<Slider>();
        SetMaxHealth();
    }

    private void SetMaxHealth()
    {
        playerHealthBar.maxValue = player.MaxHealth;
        playerHealthBar.value = player.CurrentHealth;
    }

    private void SetHealth(float health)
    {
        playerHealthBar.value = health;
    }

    private void OnEnable()
    {
        PlayerCombat.OnHurt += SetHealth;
    }

    private void OnDisable()
    {
        PlayerCombat.OnHurt -= SetHealth;
    }
}
