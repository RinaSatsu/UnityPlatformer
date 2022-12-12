using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : Enemy
{
    protected float dir = 1;
    protected Rigidbody2D rb;

    [SerializeField] protected float moveSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Turns in ooposite direction when reached end of platform
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            dir = -dir;
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        }
    }

    /// <summary>
    /// Turns in ooposite direction when reached player or another enemy
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            dir = -dir;
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(1);
                var direct = (collision.gameObject.transform.position - gameObject.transform.position);
                direct.y = 0.2f;
                direct.x = direct.x > 0 ? 0.2f : -0.2f;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direct.normalized * pushForce * 100);
            }
        }

    }

    /// <summary>
    /// Moves in set direction
    /// </summary>
    protected void Move()
    {
        Vector2 direction = new Vector2(dir * moveSpeed, rb.velocity.y);
        rb.velocity = direction;
    }
}
