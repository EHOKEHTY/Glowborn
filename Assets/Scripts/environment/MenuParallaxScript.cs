using UnityEngine;

public class MenuParallaxScript : MonoBehaviour
{
    private float startPosX;
    private float spriteWidth;

    public float scrollSpeed = 1f;

    void Start()
    {
        startPosX = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float newX = transform.position.x - scrollSpeed * Time.deltaTime;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (transform.position.x < startPosX - spriteWidth)
        {
            transform.position = new Vector3(transform.position.x + spriteWidth * 2, transform.position.y, transform.position.z);
            startPosX = transform.position.x - spriteWidth;
        }
    }
}