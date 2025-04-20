using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;
    private float movingSpeed = 5f;
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    Vector2 inputVector;


    public void Start()
    {
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
    }

    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        HandleMovment();
    }

    private void HandleMovment()
    {

        //inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + movingSpeed * Time.fixedDeltaTime * inputVector);
        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
            isRunning = true;
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
}
