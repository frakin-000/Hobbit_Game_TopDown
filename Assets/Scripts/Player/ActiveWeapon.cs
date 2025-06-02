using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }
    [SerializeField] private PlayerAttack playerAttack;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //FollowMousePosition();
        var transformFlip = PlayerVisual.Instence.lastFlipDirection;
        TransforUpdate(transformFlip);

    }

    public PlayerAttack GetActiveWeapon()
    {
        return playerAttack;
    }

    //private void FollowMousePosition()
    //{
    //    var mousePos = GameInput.Instance.GetMousePosition();
    //    var playerPos = Player.Instance.GetPlayerPossition();

    //    if (mousePos.x < playerPos.x)
    //        transform.rotation = Quaternion.Euler(0, 180, 0);
    //    else
    //        transform.rotation = Quaternion.Euler(0, 0, 0);
    //}

    private void TransforUpdate(bool turn)
    {
        if (turn)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
