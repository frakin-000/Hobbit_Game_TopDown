using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeScene : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int nextLevel;

    private void Start()
    {
        StartCoroutine(Level());
    }

    IEnumerator Level()
    {
        yield return new WaitForSeconds(5);

        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(nextLevel);
        //animator.SetTrigger("Start");
    }
}
