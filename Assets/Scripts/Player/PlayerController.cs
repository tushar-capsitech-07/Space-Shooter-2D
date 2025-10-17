using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;

    public float maxHealth = 100f;
    public float playerHealth = 100f;

    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;
    private Vector2 screenBounds;

    [SerializeField] private float speed = 2f;
    [SerializeField] private Image playerHealthUI;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private TextMeshProUGUI playerHealthText;

   

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
            BurstFire();
        }
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * speed * Time.deltaTime);

        float xClamp = Mathf.Clamp(transform.position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float yClamp = Mathf.Clamp(transform.position.y, -screenBounds.y + objectHeight, -1);

        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    public void FireBullet()
    {
        Vector3 spawnPos = transform.position;
        GameObject bullet = Instantiate(playerBulletPrefab, spawnPos, Quaternion.identity);
        Destroy(bullet, 2f);
    }

    public void BurstFire()
    {
        Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0f, 0f, 15f));
        Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
        Instantiate(playerBulletPrefab, transform.position, Quaternion.Euler(0f, 0f, -15f));
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Max(playerHealth, 0f);
        UpdateHealthUI();

        if (playerHealth <= 0)
        {
            PanelManager.Instance.ShowGameOverPanel();
            Destroy(gameObject);
        }
    }

    private void UpdateHealthUI()
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = "Health: " + playerHealth;
        }

        if (playerHealthUI != null)
        {
            playerHealthUI.fillAmount = playerHealth / maxHealth;
        }
    }

    void HealPlayer()
    {
        if (playerHealth < maxHealth)
        {
            playerHealth = Mathf.Min(playerHealth + 1, maxHealth);
            UpdateHealthUI();
        }
    }


}
