using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 0.5f;

    private Camera mainCamera;
    private float xMin, xMax, ySpawn;

    void Start()
    {
        mainCamera = Camera.main;

        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x + 0.5f;
        xMax = screenBounds.x - 0.5f;
        ySpawn = screenBounds.y + 1f; 

        InvokeRepeating(nameof(SpawnEnemy), spawnRate, spawnRate);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(xMin, xMax);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

   
}
