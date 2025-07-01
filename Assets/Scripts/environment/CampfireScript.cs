using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    [SerializeField] GameObject FireObj;
    [SerializeField] int HealValue;
    [SerializeField] float HealInterval;
    private float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FireObj.SetActive(true);
        }
        SaveController.SavePlayer(collision.GetComponent<Player>());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.tag == "Player")
            {
                timer += Time.deltaTime;
                if (timer >= HealInterval)
                {
                    Player.Instance.Heal(HealValue);
                    timer = 0f;
                }
            }
        }
    }
}
