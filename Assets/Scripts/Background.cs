using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;


    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.y / 2;
    }

    public float scrollSpeed = 2f; 

    void Update()
    {
   
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
       
        if (transform.position.y < startPos.y - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
