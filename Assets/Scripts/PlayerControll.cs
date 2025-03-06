using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    private bool isGrounded;

    void Update()
    {
        // Движение влево/вправо
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, стоит ли огонёк на земле
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
