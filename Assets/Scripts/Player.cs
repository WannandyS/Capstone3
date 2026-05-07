using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Specialized;

public class Player : MonoBehaviour
{
    public int maxHealth = 5;
    public Slider healthBar;

    public float moveSpeed = 2.5f;
    public float acceleration = .1f;
    public float jumpHeight = 15f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius = .2f;
    public LayerMask whatIsGround;

    [Header("Shooting")]
    public Transform shootPoint;
    public float distance = 10f;
    public LayerMask whatIsEnemy;
    public LineRenderer lr;

    public GameObject explosionPrefab;
    private Transform explosionPoint;
    public GameObject hitEffect;

    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        isGround = true;
        rb = this.GetComponent<Rigidbody2D>();
        lr.enabled = false;
        animator = this.GetComponent<Animator>();
        explosionPoint = transform.GetChild(2).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0 || transform.position.y <= -5.5f)
        {
            Died();
        }
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

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        lr.enabled = true;
        lr.SetPosition(0, shootPoint.position);
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.right, distance, whatIsEnemy);

        if (hit)
        {
            if (hit.transform.GetComponent<Enemy>() != null)
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(1);
            } else if (hit.transform.GetComponent<Animator>() != null && hit.transform.GetComponent<Player>() == null)
            {
                hit.transform.GetComponent<Animator>().SetTrigger("Destroy");
                Destroy(hit.transform.gameObject, .4f);
            }
            lr.SetPosition(1, hit.point);
            GameObject tempEffect = Instantiate(hitEffect, hit.point, Quaternion.identity);
            Destroy(tempEffect, .6f);
        } else
        {
            lr.SetPosition(1, shootPoint.position + shootPoint.right * distance);
        }
        
        yield return new WaitForSeconds(.1f);
        lr.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (maxHealth <= 0)
        {
            return;
        }
        maxHealth -= damage;
        healthBar.value = maxHealth;
        CameraShake.instance.Shake();
    }

    public void Died()
    {
        Debug.Log("player died");
        //animator.SetTrigger("Death");
        Destroy(this.gameObject);
        GameObject tempEffect = Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
        Destroy(tempEffect, 1f);
        FindAnyObjectByType<GameManager>().isPlayerAlive = false;
        CameraShake.instance.Shake();
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
        if (collision.gameObject.tag == "Heart")
        {
            if (maxHealth < 5)
            {
                maxHealth++;
                collision.gameObject.GetComponent<Animator>().SetTrigger("Collect");
                Destroy(collision.gameObject, .3f);
            }
        }

        if (collision.gameObject.tag == "Box")
        {
            FindAnyObjectByType<Spawner>().Spawn();
        }

        if (collision.gameObject.tag == "Traps")
        {
            Died();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            if (GameManager.instance.isPlayerAlive == false)
            {
                return;
            } else
            {
                Destroy(collision.gameObject, 3f);
            }
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
