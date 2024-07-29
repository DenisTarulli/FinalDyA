using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackHitDelay;
    [SerializeField] private float extraDelayUntilSpriteFlip;
    [SerializeField] private float attackRange;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;
    private float currentHealth;
    private float nextTimeToAttack;

    public static bool isAttacking;

    private void Start()
    {
        currentHealth = maxHealth;
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

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(extraDelayUntilSpriteFlip);

        isAttacking = false;
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return; 

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
