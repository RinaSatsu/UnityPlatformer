using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool isWaiting = false;
    private Vector3 maxDist;
    private Vector3 nextPos;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 2;
    [SerializeField] private float waitTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        maxDist = gameObject.transform.GetChild(0).position;
        nextPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Moves platform
    /// </summary>
    private void Move()
    {
        if (isWaiting)
        {
            return;
        }
        //rb.MovePosition(Vector2.MoveTowards(gameObject.transform.GetChild(0).position, nextPos, speed * Time.deltaTime));

        gameObject.transform.GetChild(0).position = Vector2.MoveTowards(gameObject.transform.GetChild(0).position, nextPos, speed * Time.deltaTime);
        if (gameObject.transform.GetChild(0).position.x == nextPos.x && gameObject.transform.GetChild(0).position.y == nextPos.y)
        {
            isWaiting = true;
            Invoke("Wait", waitTime);
        }
    }

    /// <summary>
    /// Waits in one of end positions
    /// </summary>
    private void Wait()
    {
        isWaiting = false;
        nextPos = nextPos != maxDist ? maxDist : gameObject.transform.position;
    }
    
}
