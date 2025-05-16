using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScenes : MonoBehaviour
{

    [SerializeField] private int indexScene;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadSceneAsync(indexScene);
    }
}
