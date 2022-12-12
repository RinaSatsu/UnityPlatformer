using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int hp = 1;
    [SerializeField] protected int pushForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Collides with player, hurts him and pushes off
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            var dir = (collision.gameObject.transform.position - gameObject.transform.position);
            //Труъ отталкивание, но мне не нравится, как оно работает
            //float wearoff = 1 - (dir.magnitude / 30f);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized  * pushForce * wearoff * 100);
            dir.y = 0.2f;
            dir.x = dir.x > 0 ? 0.2f : -0.2f;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * pushForce *  100);
        }
    }

    /// <summary>
    /// Taking damage and dying
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }


}
