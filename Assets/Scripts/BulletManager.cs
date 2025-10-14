using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] private float bulletPos;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject);

            ScoreManager.Instance.AddScore();
        }

        
    }
}
