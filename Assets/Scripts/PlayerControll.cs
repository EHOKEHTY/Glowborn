using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("��������")]
    public float moveSpeed = 10f;

    [Header("������")]
    public float jumpForce = 15f;
    public float jumpHoldTime = 0.2f; // ������� �������� ������� ������ ������ ��� �������
    public float minJumpForce = 5f;

    [Header("����� � �����")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private float coyoteCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;

    void Update()
    {
        // == �������������� �������� ==
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // == ����� ������ (����� ����� ������ � ���������, ����� ��� ����� ��������) ==
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // == ����� ������ (����� ����� ������� ������� �� ������������ ������) ==
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // == ������ (������ �������) ==
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpHoldTime;

            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }

        // == ��������� ������ (���� ������ ������ � ���������� ������) ==
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // == �������� ������ � ���������� ������ ==
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;

            // ������������� ��������� ������, �������� ������ ����������� ����
            if (rb.velocity.y > minJumpForce)
            {
                rb.velocity = new Vector2(rb.velocity.x, minJumpForce);
            }
        }

        // ���� ����� ����� ������ � ���������� isJumping
        if (rb.velocity.y <= 0)
        {
            isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        // ����� �������� �� ����� ������ �������� � Raycast ��� GroundCheck
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.31f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
