using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartDialog : MonoBehaviour
{
    public static StartDialog Instance { get; private set; }

    public GameObject Dialog;

    private void Start()
    {
        Dialog.SetActive(false);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Dialog.SetActive(true);
            GameInput.Instance.DisableMovement();
        }
    }

    public void EndDialog()
    {
        Dialog.SetActive(false);
        GameInput.Instance.EnableMovement();
        Player.Instance.TakeDeath();
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2);

        GameInput.Instance.EnableMovement();
        Player.Instance.TakeDeath();
    }
}
