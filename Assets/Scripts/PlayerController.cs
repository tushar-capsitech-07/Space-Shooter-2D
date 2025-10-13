using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject playerBulletPrefab;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * speed * Time.deltaTime);

        float xClamp = Mathf.Clamp(transform.position.x, -11.7f, 11.7f);
        float yClamp = Mathf.Clamp(transform.position.y, -4f, 4f);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    void FireBullet()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0);
        GameObject bullet = Instantiate(playerBulletPrefab, spawnPos, Quaternion.identity);
        Destroy(bullet, 2f);
    }





}
