using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int enemyDamage = 10;
    private int currentHealth;

    private PolygonCollider2D pollygonCollider2D;
    private BoxCollider2D boxCollider2D;
    private EnemyAi enemyAi;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private void Awake()
    {
        pollygonCollider2D = GetComponent<PolygonCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyAi = GetComponent<EnemyAi>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            player.TakeDamage(transform, enemyDamage);
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
            pollygonCollider2D.enabled = false;

            enemyAi.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);
            EnemyDie.Instance.IsEnemyDie();

        }
    }

    public void PolygonColliderTurnOff()
    {
        pollygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        pollygonCollider2D.enabled = true;
    }
}
