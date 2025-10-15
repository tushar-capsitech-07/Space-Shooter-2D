using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private TextMeshProUGUI playerHealthText;

    public float playerHealth = 100f;

    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        mainCamera = Camera.main;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }

        screenBounds = mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)
        );

        UpdateHealthUI();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BurstFire(); // Fires 3 bullets at once
        }
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * speed * Time.deltaTime);

        float xClamp = Mathf.Clamp(transform.position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float yClamp = Mathf.Clamp(transform.position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    public void FireBullet()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, 0.5f, 0);
        GameObject bullet = Instantiate(playerBulletPrefab, spawnPos, Quaternion.identity);
        Destroy(bullet, 2f);
    }

    public void BurstFire()
    {
        Instantiate(playerBulletPrefab, transform.position + new Vector3(-0.3f, 0.5f, 0f), Quaternion.identity);
        Instantiate(playerBulletPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Instantiate(playerBulletPrefab, transform.position + new Vector3(0.3f, 0.5f, 0f), Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Max(playerHealth, 0);

        UpdateHealthUI();

        if (playerHealth <= 0)
        {
            PanelManager.Instance.ShowGameOverPanel();
            Destroy(gameObject, 2f);
        }
    }

    private void UpdateHealthUI()
    {
        playerHealthText.text = "Health: " + playerHealth;
        Debug.Log("Player Health: " + playerHealth);
    }
}
