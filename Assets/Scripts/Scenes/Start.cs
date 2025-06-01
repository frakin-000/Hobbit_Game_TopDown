using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public static StartGame Instence { get; private set; }

    [SerializeField] private Animator animator;

    private void Start()
    {
        Instence = this;
    }

    public void GameStart()
    {
        StartCoroutine(Level());
    }


    IEnumerator Level()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);
    }
}
