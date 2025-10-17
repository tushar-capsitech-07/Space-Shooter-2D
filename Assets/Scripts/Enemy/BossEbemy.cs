using UnityEngine;

public class BossEbemy : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private float bossHealth = 100f;
    [SerializeField] private float damagePerBullet = 10f; 

    [SerializeField] private ParticleSystem EnemyExpla;
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
        else if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            bossHealth -= damagePerBullet;

            if (bossHealth <= 0f)
            {
                EnemyExpla = Instantiate(EnemyExpla, transform.position, Quaternion.identity);
                EnemyExpla.Play();
                Destroy(EnemyExpla.gameObject, EnemyExpla.main.duration + EnemyExpla.main.startLifetime.constantMax);

                ScoreManager.Instance.AddScore();
                Destroy(gameObject);
            }
        }
    }

    void FireBullet()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, -1f, 0);
        GameObject bullet = Instantiate(enemyBulletPrefab, spawnPos, Quaternion.identity);
        Destroy(bullet, 2f);
    }
}