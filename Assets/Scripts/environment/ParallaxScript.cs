using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    private float startPos, length;
    public GameObject camera;
    public float paralaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        //float y = camera.transform.position.y;
        float temp = camera.transform.position.x * (1 - paralaxEffect);
        float dist = camera.transform.position.x * paralaxEffect;

        // двигаем фон с поправкой на paralaxEffect
        transform.position = new Vector3(startPos + dist, camera.transform.position.y, transform.position.z);

        // если камера перескочила спрайт, то меняем startPos
        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}