using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private GameObject enemyBulletPrefab;

    void Start()
    {
        InvokeRepeating(nameof(FireBullet), spawnRate, spawnRate);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PanelManager.Instance.ShowGameOverPanel();
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
    }
    void FireBullet()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, -1f, 0);
        GameObject bullet = Instantiate(enemyBulletPrefab, spawnPos, Quaternion.identity);
        Destroy(bullet, 2f);
    }


}



