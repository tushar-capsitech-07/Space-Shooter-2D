using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Camera mainCamera;
    private float xMin, xMax, ySpawn;

    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private float bossSpawnRate = 20f;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossEnemyPrefab;

    void Start()
    {
        mainCamera = Camera.main;

        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x + 0.5f;
        xMax = screenBounds.x - 0.5f;
        ySpawn = screenBounds.y + 1f;

        InvokeRepeating(nameof(SpawnEnemy), spawnRate, spawnRate);
        InvokeRepeating(nameof(SpawnBossEnemy), bossSpawnRate, bossSpawnRate);
    }

    void SpawnEnemy()   
    {
        Spawn(enemyPrefab);
    }

    void SpawnBossEnemy()
    {
        Spawn(bossEnemyPrefab);
    }

    void Spawn(GameObject prefab)
    {
        float randomX = Random.Range(xMin, xMax);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, 0f);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}


    