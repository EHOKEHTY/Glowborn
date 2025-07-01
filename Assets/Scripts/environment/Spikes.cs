using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour
{
    public int damagePerSecond = 1;
    public float damageInterval = 1f;
    public float damageTimer = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(damagePerSecond);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                Player.Instance.TakeDamage(damagePerSecond);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}
