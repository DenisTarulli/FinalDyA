using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackHitDelay;
    [SerializeField] private float extraDelayUntilSpriteFlip;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;
    private float nextTimeToAttack;

    public static bool isAttacking;

    [SerializeField] private PlayerStatsScriptableObject playerStats;

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
        playerStats.DecreaseHealth(damageToTake);

        if (playerStats.currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return; 

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
