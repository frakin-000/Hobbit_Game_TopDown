using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int maxHealth = 10;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    private Rigidbody2D rb;
    private float movingSpeed = 8f;
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    Vector2 inputVector;
    private KnockBack knockBack;
    private int currentHealth;
    private float damageRecoveryTime = 0.5f;
    private bool canTakeDamage;
    private bool isAlive = true;
    private Vector3 startPosition;

    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerTakeHit;

    public void Start()
    {
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
        currentHealth = maxHealth;
        canTakeDamage = true;
        startPosition = transform.position;
    }

    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        HealthVisual();
        if (knockBack.IsGettingKnockedBack)
            return;
        HandleMovment();
    }

    public void HealthVisual()
    {
        var heartsShow = currentHealth;

        for (var i = 0; i < hearts.Length; i++)
        {
            if (2 * i < heartsShow - 1)
                hearts[i].sprite = fullHeart;
            else if (2 * i < heartsShow)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (canTakeDamage && isAlive)
        {
            canTakeDamage = false;
            currentHealth = Mathf.Max(0, currentHealth -= damage);
            OnPlayerTakeHit?.Invoke(this, EventArgs.Empty);
            knockBack.GetKnockedBack(damageSource);
            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    public void TakeDeath(int damage)
    {
        currentHealth -= damage;
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth == 0)
        {
            isAlive = false;
            knockBack.StopKnockMovement();
            GameInput.Instance.DisableMovement();
            //OnPlayerTakeHit?.Invoke(this, EventArgs.Empty);
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);

            //Restart();
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void HandleMovment()
    {

        //inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + movingSpeed * Time.fixedDeltaTime * inputVector);
        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
            PlayerVisual.Instence.PlayerFaceFlip(inputVector.x);
            isRunning = true;
        }
        else
            isRunning = false;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector3 GetPlayerPossition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Restart()
    {
        isAlive = true;
        currentHealth = maxHealth;
        transform.position = startPosition;
        knockBack.StartKnockMovement();
        GameInput.Instance.EnableMovement();
        PlayerVisual.Instence.RestartLife();

    }
}
