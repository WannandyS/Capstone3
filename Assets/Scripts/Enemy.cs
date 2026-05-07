using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;

    private Transform player;
    public float attackRange = 5f;
    public float startTime = 2f;
    private float timeBetweenShoot;

    private Animator animator;
    public GameObject floatingTextPrefab;

    [Header("Shooting")]
    public Transform shootPoint;
    public float distance = 10f;
    public LayerMask whatIsPlayer;
    public LineRenderer lr;

    public GameObject hitEffect;

    void Start()
    {
        timeBetweenShoot = startTime;
        lr = this.GetComponent<LineRenderer>();
        animator = this.GetComponent<Animator>();
        lr.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0)
        {
            return;
        }

        if (GameManager.instance.isPlayerAlive == false)
        {
            return;
        }

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (timeBetweenShoot <= 0f)
            {
                StartCoroutine(Shoot());
                timeBetweenShoot = startTime;
            } else
            {
                timeBetweenShoot -= Time.deltaTime;
            }
        }
    }

    IEnumerator Shoot()
    {
        lr.enabled = true;
        lr.SetPosition(0, shootPoint.position);
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.left, distance, whatIsPlayer);

        if (hit)
        {
            if (hit.transform.GetComponent<Player>() != null)
            {
                hit.transform.GetComponent<Player>().TakeDamage(1);
            }
            lr.SetPosition(1, hit.point);
            GameObject tempEffect = Instantiate(hitEffect, hit.point, Quaternion.identity);
            Destroy(tempEffect, .8f);
        }
        else
        {
            lr.SetPosition(1, shootPoint.position + (-shootPoint.right * distance));
        }

        yield return new WaitForSeconds(.1f);
        lr.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (maxHealth <= 0)
        {
            Died();
            Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            return;
        }
        maxHealth -= damage;
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        animator.SetTrigger("Hurt");
        CameraShake.instance.Shake();
    }

    void Died()
    {
        Debug.Log(this.transform.name + " Died");
        animator.SetTrigger("Died");
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        Destroy(this.gameObject, 5f);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (-transform.right * attackRange), .2f);
    }
}
