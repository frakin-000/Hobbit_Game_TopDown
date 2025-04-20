using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private PolygonCollider2D pollygonCollider2D;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private void Awake()
    {
        pollygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        
        DetectDeath();
    }
    private void DetectDeath()
    {
        if (currentHealth <= 0)
            OnDeath?.Invoke(this, EventArgs.Empty);
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
