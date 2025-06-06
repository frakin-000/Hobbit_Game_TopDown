using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScenes : MonoBehaviour
{

    [SerializeField] private int indexScene;
    [SerializeField] private Animator animator;
    //[SerializeField] private GameObject pointer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // pointer.SetActive(false);
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Finish");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(indexScene);
    }
}
