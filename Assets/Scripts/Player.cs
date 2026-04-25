using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float acceleration = .1f;
    public float jumpHeight = 15f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius = .2f;
    public LayerMask whatIsGround;

    void Start()
    {
        isGround = true;
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed += acceleration * Time.deltaTime;
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        Collider2D collInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (collInfo)
        {
            isGround = true;
        } else
        {
            isGround = false;
        }
    }

    public void Jump()
    {
        if (isGround == true)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpHeight;
            rb.linearVelocity = velocity;
            animator.SetBool("Jump", true);
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            FindAnyObjectByType<Spawner>().Spawn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Destroy(collision.gameObject, 3f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
