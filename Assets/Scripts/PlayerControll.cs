using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Движение")]
    public float moveSpeed = 10f;

    [Header("Прыжок")]
    public float jumpForce = 15f;
    public float jumpHoldTime = 0.2f; // Сколько максимум времени длится подъём при зажатии
    public float minJumpForce = 5f;

    [Header("Буфер и койот")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private float coyoteCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;

    void Update()
    {
        // == Горизонтальное движение ==
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // == Койот таймер (время после выхода с платформы, когда ещё можно прыгнуть) ==
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // == Буфер прыжка (время после нажатия пробела до фактического прыжка) ==
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // == Прыжок (первый импульс) ==
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpHoldTime;

            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }

        // == Удержание прыжка (если зажата кнопка — продолжаем подъем) ==
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // == Отпустил пробел — прекращаем подъём ==
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;

            // Принудительно прерываем прыжок, оставляя только минимальную силу
            if (rb.velocity.y > minJumpForce)
            {
                rb.velocity = new Vector2(rb.velocity.x, minJumpForce);
            }
        }

        // Если игрок начал падать — сбрасываем isJumping
        if (rb.velocity.y <= 0)
        {
            isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        // Можно заменить на более точную проверку с Raycast или GroundCheck
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.31f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
