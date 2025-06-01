using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int enemyDamage = 10;
    private PolygonCollider2D pollygonCollider2D;

    private void Awake()
    {
        pollygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void PolygonColliderTurnOff()
    {
        pollygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        pollygonCollider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, enemyDamage);
        }
    }

    public void PolygonEnabled()
    {
        pollygonCollider2D.enabled = false;
    }
}
