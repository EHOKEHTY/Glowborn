using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody2D rb;

    [Header("Швидкість руху")]
    public float moveSpeed = 10f;

    [Header("Стрибок")]
    public float jumpForce = 15f;
    public float jumpHoldTime = 0.2f;
    public float minJumpForce = 5f;

    [Header("Буфер і койот")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    [Header("Здоров'я")]
    public int maxHealth = 3;
    public int currentHealth;
    public ParticleSystem DeathParts;


    public float respawnDelay = 1f;
    private bool isDead = false;


    private float coyoteCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //SaveController.LoadPlayer(this);
        UIHealth.Instance.UpdateHearts(currentHealth); // оновлення UI при старті
    }

    void Update()
    {

        // == Горизонтальний рух ==
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // == Койот-таймер ==
        if (IsGrounded())
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // == Буфер стрибка ==
        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // == Стрибок ==
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpHoldTime;

            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }

        // == Утримання стрибка ==
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // == Відпускання стрибка ==
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;

            if (rb.velocity.y > minJumpForce)
                rb.velocity = new Vector2(rb.velocity.x, minJumpForce);
        }

        if (rb.velocity.y <= 0)
            isJumping = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.31f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UIHealth.Instance.UpdateHearts(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UIHealth.Instance.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Instantiate(DeathParts, this.transform);
        //DeathParts.Play();

        GetComponentInChildren<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        GetComponent<Collider2D>().enabled = false;
        rb.simulated = false;

        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        SaveController.LoadPlayer(this);

        currentHealth = maxHealth;
        UIHealth.Instance.UpdateHearts(currentHealth);

        GetComponentInChildren<ParticleSystem>().Play();

        GetComponent<Collider2D>().enabled = true;
        rb.simulated = true;

        isDead = false;
    }

}
