using UnityEngine;

public class PlayerAttac : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attac();
    }

    private void Attac()
    {
        animator.SetTrigger("IsAttac");
    }
}
