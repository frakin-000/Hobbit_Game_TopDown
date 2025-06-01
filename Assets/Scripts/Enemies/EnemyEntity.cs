using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private BoxCollider2D boxCollider2D;
    private EnemyAi enemyAi;
    private EnemyAttack enemyAttack;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyAi = GetComponent<EnemyAi>();
        enemyAttack = GetComponent<EnemyAttack>();

    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            boxCollider2D.enabled = false;
            enemyAttack.PolygonEnabled();
            enemyAi.SetDeathState();
            OnDeath?.Invoke(this, EventArgs.Empty);
            EnemyDie.Instance.IsEnemyDie();

        }
    }
}
