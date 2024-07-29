using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackHitDelay;
    [SerializeField] private float attackTriggerDistance;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float extraDelayUntilSpriteFlip;
    private Animator animator;
    private float nextTimeToAttack;
    private float distanceToPlayer;
    private int currentHealth;
    private bool isAttacking;
    private Transform playerPosition;
    private EnemyHealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
        playerPosition = FindObjectOfType<PlayerCombat>().transform;
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (playerPosition == null) return;

        distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);
        
        if (distanceToPlayer <= attackTriggerDistance && !isAttacking && Time.time >= nextTimeToAttack)
        {
            TriggerAttack();
        }
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (!isAttacking)
            animator.SetTrigger("Hurt");
    }

    private void TriggerAttack()
    {
        isAttacking = true;
        nextTimeToAttack = Time.time + 1f / attackSpeed;

        animator.SetTrigger("Attack");

        StartCoroutine(AttackAction());
    }

    private IEnumerator AttackAction()
    {
        yield return new WaitForSeconds(attackHitDelay);

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(extraDelayUntilSpriteFlip);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        healthBar.enabled = false;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}