using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackHitDelay;
    [SerializeField] private float extraDelayUntilSpriteFlip;
    private Animator animator;
    private float nextTimeToAttack;

    public static bool isAttacking;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToAttack)
            TriggerAttack();
    }

    private void TriggerAttack()
    {
        nextTimeToAttack = Time.time + 1f / attackSpeed;
        animator.SetTrigger("Attack");

        StartCoroutine(AttackAction());
    }

    private IEnumerator AttackAction()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackHitDelay);

        // Check hits

        yield return new WaitForSeconds(extraDelayUntilSpriteFlip);

        isAttacking = false;
    }
}
