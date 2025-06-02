using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndControl : MonoBehaviour
{
    public GameObject end;

    private void Start()
    {
        StartCoroutine(Level());
    }

    IEnumerator Level()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(0);
    }

}
