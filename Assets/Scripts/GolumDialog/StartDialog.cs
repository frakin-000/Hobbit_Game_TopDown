using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartDialog : MonoBehaviour
{
    public static StartDialog Instance { get; private set; }

    public GameObject Dialog;

    private bool isDialog;

    private void Start()
    {
        Dialog.SetActive(false);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && isDialog)
        {
            Dialog.SetActive(true);
            GameInput.Instance.DisableMovement();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isDialog = true;
    }

    public void EndDialog(int damage)
    {
        Dialog.SetActive(false);
        isDialog = false;
        StartCoroutine(End(damage));  
    }

    IEnumerator End(int damage)
    {
        yield return new WaitForSeconds(2);

        GameInput.Instance.EnableMovement();
        Player.Instance.TakeDeath(damage);
    }
}
