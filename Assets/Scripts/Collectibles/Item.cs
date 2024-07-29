using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject collectedEffect;
    [SerializeField] private float collectedEffectDuration;
    [SerializeField] private int scoreValue;

    [SerializeField] private PlayerStatsScriptableObject playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerStats.IncreaseScore(scoreValue);

        GameManager.Instance.DecreaseCollectibleAmount();

        GameObject vfx = Instantiate(collectedEffect, transform.position, Quaternion.identity);
        Destroy(vfx, collectedEffectDuration);

        Destroy(gameObject);
    }
}
