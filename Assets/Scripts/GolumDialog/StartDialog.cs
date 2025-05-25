using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartDialog : MonoBehaviour
{
    public static StartDialog Instance { get; private set; }

    public GameObject Dialog;
    public GameObject Golum;

    private bool isDialog;

    private bool restart;

    private void Start()
    {
        Dialog.SetActive(false);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && isDialog && !restart)
        {
            Dialog.SetActive(true);
            GameInput.Instance.DisableMovement();
        }

        if (restart)
            Golum.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isDialog = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isDialog = false;
    }

    public void EndDialog(int damage, bool victory)
    {
        restart = victory;
        Dialog.SetActive(false);
        isDialog = false;
        if (victory)
            EnemyDie.Instance.IsEnemyDie();
        StartCoroutine(End(damage));
    }

    IEnumerator End(int damage)
    {
        yield return new WaitForSeconds(2);

        GameInput.Instance.EnableMovement();
        Player.Instance.TakeDeath(damage);
    }
}
