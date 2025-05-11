using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScenes : MonoBehaviour
{
    private static bool enemyAlive = true;

    public static void EnemyDefeat()
    {
        enemyAlive = false;
    }
    
    void Update()
    {
        if(!enemyAlive && SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("DarkForest");
        }
    }
}
