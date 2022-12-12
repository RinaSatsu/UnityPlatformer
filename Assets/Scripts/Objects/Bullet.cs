using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    private Rigidbody2D rb;
    private Vector2 direction;

    [SerializeField] private float moveSpeed = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Flip sprite if needed
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX;
        gameObject.transform.SetParent(null);
        gameObject.transform.localScale = Vector3.one * 0.75f;
        int dir = gameObject.GetComponent<SpriteRenderer>().flipX ? -1 : 1;
        direction = new Vector2(dir, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Collides with ground, with enemy and hurts him
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !collision.CompareTag("Vision"))
        {
            collision.GetComponent<Enemy>()?.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Moves object in set direction
    /// </summary>
    private void Move()
    {
        rb.velocity = direction * moveSpeed;
    }   
}
