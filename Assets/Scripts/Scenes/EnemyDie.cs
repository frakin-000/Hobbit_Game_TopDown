using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public static EnemyDie Instance { get; private set; }

    [SerializeField] private int CountEnemyAlive;
    public GameObject NextLevel;
    public void IsEnemyDie()
    {
        CountEnemyAlive -= 1;
        if (CountEnemyAlive == 0)
        {
            NextLevel.SetActive(true);
        }
    }

    private void Start()
    {
        NextLevel.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
    }
}
