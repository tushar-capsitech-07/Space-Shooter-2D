using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float xMin = -10.7f;
    [SerializeField] private float xMax = 10.7f;
    [SerializeField] private float ySpawn = 6f;

    [SerializeField] private float spawnRate = 0.5f; 



    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnRate, spawnRate);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(xMin, xMax);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
