using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockForce=3f;
    [SerializeField] private float knockMovingTimerMax=0.3f;

    private float knockMovingTimer;
    private Rigidbody2D rb;

    public bool IsGettingKnockedBack { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        knockMovingTimer -= Time.deltaTime;
        if (knockMovingTimer < 0)
            StopKnockMovement();
    }
    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingKnockedBack = true;
        knockMovingTimer = knockMovingTimerMax;
        var difference = (transform.position - damageSource.position).normalized * knockForce / rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
    }

    public void StopKnockMovement()
    {
        rb.linearVelocity = Vector2.zero;
        IsGettingKnockedBack = false;
        //rb.velocity= Vector2.zero;
    }
}
