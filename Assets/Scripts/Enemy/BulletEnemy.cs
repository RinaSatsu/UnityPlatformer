using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 direction;

    [SerializeField] private float moveSpeed = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        direction = transform.position - transform.parent.position;
        gameObject.transform.SetParent(null);
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
    /// Collides with ground, with player and hurts him
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collider.gameObject.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Moves bullet in set direction
    /// </summary>
    private void Move()
    {
        rb.velocity = direction * moveSpeed;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
}
