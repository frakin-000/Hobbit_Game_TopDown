using UnityEngine;

public class OrcVisual : MonoBehaviour
{
    [SerializeField] private EnemyAi enemyAi;
    [SerializeField] private EnemyEntity enemyEntity;
    private Animator animator;
    private const string IsRunning = "IsRunning";
    private const string ChasingSpeedMultiplier = "ChasingSpeedMultiplier";
    private const string Attack = "Attack";
    private const string TakeHit = "TakeHit";
    private const string IsDie = "IsDie";

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyAi.OnEnemyAttack += EnemyAi_OnEnemyAttack;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IsDie, true);
        spriteRenderer.sortingOrder = -1;
    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TakeHit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attack");
    }

    private void EnemyAi_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(Attack);
    }

    private void OnDestroy()
    {
        enemyAi.OnEnemyAttack -= EnemyAi_OnEnemyAttack;
    }

    private void Update()
    {
        animator.SetBool(IsRunning, enemyAi.IsRunning());
        animator.SetFloat(ChasingSpeedMultiplier, enemyAi.GetRoamingAnimationSpeed());
    }

    public void TriggerAttackingAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    public void TriggerAttackingAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }
}
