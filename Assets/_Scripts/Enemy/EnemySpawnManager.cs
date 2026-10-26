using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemies;
    [Space]
    [Tooltip("In Seconds")]
    public float spawnInterval; // In seconds


    // Local Variable
    int enemyIndex;

    private void Start()
    {
        enemyIndex = 0;
    }

    // Spawns enemies after the interval
    public IEnumerator EnemySpawn_C()
    {
        while (enemyIndex < enemies.Length)
        {
            enemies[enemyIndex].SetActive(true);
            enemyIndex++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
