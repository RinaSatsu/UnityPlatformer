using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private enum States { idle, run, jump, hurt, fire };

    private int maxHp = 10;
    private int hp;
    private float lastDir = 1.0f;
    private float nextBullet;
    private bool hitEnemy2D = false;
    private bool onGround;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator animator;
    private LayerMask environmentLayerMask;
    //private States state;


    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float bulletCd = 1.0f;

    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform spawnPoint;
    public Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        environmentLayerMask = LayerMask.GetMask("Ground");
        hp = maxHp;
        nextBullet = Time.time + bulletCd;
    }

    private void FixedUpdate()
    {
        if (hitEnemy2D)
            return;
        CheckGround();
        animator.SetBool("isJumping", !onGround);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Vertical") > 0)
        {
            if (onGround || hitEnemy2D)
            {
                Jump();
            }
        }
        Move();
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextBullet)
        {
            Shoot();
        }
    }

    /// <summary>
    /// Collision with enemy - detector, collision with moving object - ties to that object
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            hitEnemy2D = true;
        }
        if (collision.gameObject.CompareTag("Moving"))
        {
            gameObject.transform.SetParent(collision.transform);
        }
    }

    /// <summary>
    /// Unties from moving object
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moving"))
        {
            gameObject.transform.SetParent(null);
        }
    }

    /// <summary>
    /// Moves character according to input on axis "x"
    /// </summary>
    private void Move()
    {
        Vector2 offset = Vector2.zero;
        if (gameObject.transform.parent != null)
        {
            offset = gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal != 0 && lastDir != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (moveHorizontal * lastDir < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            lastDir = moveHorizontal;
        }
        Vector2 direction = new Vector2(moveHorizontal * moveSpeed + offset.x, rb.velocity.y);
        rb.velocity = direction;
    }

    /// <summary>
    /// Jumping
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Shoots projectile object
    /// </summary>
    private void Shoot()
    {
        animator.SetBool("isFiring", true);
        var bullet = GameObject.Instantiate(bulletObj, spawnPoint.position, Quaternion.identity, gameObject.transform);
        nextBullet = Time.time + bulletCd;
        Destroy(bullet, 3);
        Invoke("StopFiring", 0.2f);
    }

    /// <summary>
    /// Checks if object is on ground
    /// </summary>
    private void CheckGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(rb.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, environmentLayerMask);
        onGround = hit2D;
    }

    /// <summary>
    /// Recovery after getting hit
    /// </summary>
    private void NotHit()
    {
        hitEnemy2D = false;
        animator.SetBool("isHurt", false);
        audioSource.Stop();
    }

    /// <summary>
    /// Recovery after firing
    /// </summary>
    private void StopFiring()
    {
        animator.SetBool("isFiring", false);
    }

    /// <summary>
    /// Taking damage and dying
    /// </summary>
    /// <param name="damage">Amount of damage (heals if negative)</param>
    public void TakeDamage(int damage)
    {
        if (damage >= 0)
        {
            audioSource.Play();
            animator.SetBool("isHurt", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isFiring", false);
            Invoke("NotHit", 0.5f);
        }
        hp -= damage;
        if (hp > maxHp)
            hp = maxHp;
        if (hp <= 0)
        {
            hp = 0;
            gameObject.SetActive(false);
            Invoke("Respawn", 1f);
        }
    }

    /// <summary>
    /// Respawns player on the respawn point
    /// </summary>
    private void Respawn()
    {
        gameObject.transform.position = respawnPoint.position;
        hp = maxHp;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Returns current amount of hp
    /// </summary>
    public int GetHP()
    {
        return hp;
    }
}
