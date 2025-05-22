using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance { get; private set; }


    public int numberQuestion;
    public GameObject[] questions;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private int currentHealth = 10;

    public void Start()
    {
        Instance = this;
        questions[0].SetActive(true);
        for (var i = 1; i < questions.Length; i++)
            questions[i].SetActive(false);
    }

    public void NextQuestion()
    {
        if (numberQuestion + 1 == questions.Length)
            StartDialog.Instance.EndDialog();

        questions[numberQuestion].SetActive(false);
        numberQuestion++;
        questions[numberQuestion].SetActive(true);
    }

    public void Update()
    {
        if (currentHealth > 0)
            HealthVisual();
        else
            StartDialog.Instance.EndDialog();
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
    }
}
