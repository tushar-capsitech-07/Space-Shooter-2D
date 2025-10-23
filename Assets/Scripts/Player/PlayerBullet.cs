using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float bulletPos;
    [SerializeField] private float speed = 10f;

        
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }     
}
