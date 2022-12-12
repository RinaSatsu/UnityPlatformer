using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : EnemyMoving
{
    private enum State { standby, chase};

    private GameObject target = null;
    private State state = State.standby;

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.standby:
            {
                break;
            }
            case State.chase:
            {
                Chase();
                break;
            }
        }
    }

    /// <summary>
    /// Detects player and starts chasing it
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !collision.CompareTag("Projectile"))
        {
            target = collision.gameObject;
            state = State.chase;
        }
    }

    /// <summary>
    /// Loses player and returns to start position
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !collision.CompareTag("Projectile"))
        {
            target = null;
            state = State.standby;
        }
    }

    /// <summary>
    /// Follows player
    /// </summary>
    private void Chase()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        if (targetDirection.x * dir < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            dir = targetDirection.x > 0 ? 1 : -1;
        }
        Move();
    }
}
