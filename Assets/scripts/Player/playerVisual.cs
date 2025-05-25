using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerVisual : MonoBehaviour
{
    public static PlayerVisual Instence { get; private set; }

    [SerializeField] private PlayerAttack playerAttack;
    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string Attack = "Attack";
    private const string IsDie = "IsDie";
    private const string TakeHit = "TakeHit";
    private SpriteRenderer spriteRenderer;

    public bool lastFlipDirection = true;

    private void Start()
    {
        playerAttack.OnSwordSwing += PlayerAttack_OnSwordSwing;
        Player.Instance.OnPlayerTakeHit += Player_OnPlayerTakeHit;
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;

    }

    public void RestartLife()
    {
        animator.SetBool(IsDie, false);
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TakeHit);
        animator.SetBool(IsDie, true);
    }

    private void Player_OnPlayerTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TakeHit);
    }

    private void PlayerAttack_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(Attack);
    }

    private void Awake()
    {
        Instence = this;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        //if (Player.Instance.IsAlive())
        //    AdjustPlayerFaceFlip();
    }

    //private void AdjustPlayerFaceFlip()
    //{
    //    var mousePos = GameInput.Instance.GetMousePosition();
    //    var playerPos = Player.Instance.GetPlayerPossition();

    //    if (mousePos.x < playerPos.x)
    //        spriteRenderer.flipX = true;
    //    else
    //        spriteRenderer.flipX = false;
    //}

    public void PlayerFaceFlip(float vector)
    {
        if (vector < 0)
        {
            spriteRenderer.flipX = true;
            lastFlipDirection = true;
        }
        else if (vector == 0)
        {
            spriteRenderer.flipX = lastFlipDirection;
        }

        else
        {
            spriteRenderer.flipX = false;
            lastFlipDirection = false;
        }
    }

    public void TriggerEndAttackAnimation()
    {
        playerAttack.AttackColliderTurnOff();
    }

    public void TriggerLifeRestart()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        Player.Instance.Restart();
    }
}
