using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject collectedEffect;
    [SerializeField] private float collectedEffectDuration;
    [SerializeField] private int scoreValue;

    public static event Action<int> OnItemCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnItemCollected?.Invoke(scoreValue);

        GameObject vfx = Instantiate(collectedEffect, transform.position, Quaternion.identity);
        Destroy(vfx, collectedEffectDuration);

        Destroy(gameObject);
    }
}
