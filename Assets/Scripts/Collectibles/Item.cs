using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject collectedEffect;
    [SerializeField] private float collectedEffectDuration;

    public static event Action OnItemCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnItemCollected?.Invoke();

        GameObject vfx = Instantiate(collectedEffect, transform.position, Quaternion.identity);
        Destroy(vfx, collectedEffectDuration);

        Destroy(gameObject);
    }
}