using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerStatsScriptableObject", menuName = "ScriptableObjects/Stats Manager")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;

    public int score;

    [System.NonSerialized] public UnityEvent<int> OnHealthUpdate;
    [System.NonSerialized] public UnityEvent<int> OnScoreUpdate;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        score = 0;

        OnHealthUpdate ??= new UnityEvent<int>();
        OnScoreUpdate ??= new UnityEvent<int>();
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

        if (score < 0)
            score = 0;

        OnScoreUpdate?.Invoke(score);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void ResetScore()
    {
        score = 0;

        OnScoreUpdate?.Invoke(score);
    }
}
