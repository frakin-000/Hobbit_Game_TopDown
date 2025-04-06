using UnityEngine;

public class playerVisual : MonoBehaviour
{
    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        AdjustPlayerFaceFlip();
    }

    private void AdjustPlayerFaceFlip()
    {
        var mousePos = GameInput.Instance.GetMousePosition();
        var playerPos = Player.Instance.GetPlayerPossition();

        if (mousePos.x < playerPos.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
