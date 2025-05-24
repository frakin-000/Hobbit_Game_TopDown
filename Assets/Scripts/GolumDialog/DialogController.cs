using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance { get; private set; }


    public int numberQuestion;
    public GameObject[] questions;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public GameObject wrong;
    public GameObject right;
    public GameObject defeat;
    public GameObject victory;

    private bool start = true;


    private int currentHealth;
    private int damage;

    public void Start()
    {
        Instance = this;
        currentHealth = 10;
        start = true;
        questions[0].SetActive(true);
        for (var i = 1; i < questions.Length; i++)
            questions[i].SetActive(false);

        wrong.SetActive(false);
        right.SetActive(false);
        defeat.SetActive(false);
        victory.SetActive(false);
    }

    public void NextQuestion()
    {
        StartCoroutine(RightVisual());
    }

    IEnumerator RightVisual()
    {
        questions[numberQuestion].SetActive(false);
        if (!start)
        {
            right.SetActive(true);
            yield return new WaitForSeconds(2);
            right.SetActive(false);
        }
        if (numberQuestion + 1 == questions.Length)
            StartCoroutine(VictoryVisual());
        else
        {
            numberQuestion++;
            questions[numberQuestion].SetActive(true);
            start = false;
        }

    }

    IEnumerator VictoryVisual()
    {

        victory.SetActive(true);
        yield return new WaitForSeconds(2);
        victory.SetActive(false);
        StartDialog.Instance.EndDialog(damage);

    }

    public void Update()
    {
        if (currentHealth > 0)
            HealthVisual();
        else
            StartCoroutine(DefeatVisual());
    }

    IEnumerator DefeatVisual()
    {
        questions[numberQuestion].SetActive(false);
        defeat.SetActive(true);
        yield return new WaitForSeconds(2);
        defeat.SetActive(false);
        Start();
        StartDialog.Instance.EndDialog(damage);

    }

    private void HealthVisual()
    {
        var heartsShow = currentHealth;

        for (var i = 0; i < hearts.Length; i++)
        {
            if (2 * i < heartsShow - 1)
                hearts[i].sprite = fullHeart;
            else if (2 * i < heartsShow)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void Damage()
    {
        currentHealth -= 2;
        damage += 2;
        if (currentHealth > 0)
            StartCoroutine(WrongVisual());
    }

    IEnumerator WrongVisual()
    {
        questions[numberQuestion].SetActive(false);
        wrong.SetActive(true);
        yield return new WaitForSeconds(2);
        wrong.SetActive(false);
        questions[numberQuestion].SetActive(true);

    }
}
