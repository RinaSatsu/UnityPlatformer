using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    private bool detected = false;//не уверена, что оно надо, но, возможно чисто с target оно будет багаться
    private GameObject target = null;

    [SerializeField] private float cd = 1.0f;
    [SerializeField] private float gunSpeed = 40.0f;

    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform spawnPoint;


    private void Update()
    {
        if (target != null)
        {
            RotateAfter(target);
        }
    }

    /// <summary>
    /// Detects player and starts shooting when facing it
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !collision.CompareTag("Projectile") && collision.CompareTag("Player"))
        {
            target = collision.gameObject;
            detected = true;
            Quaternion targetRotation = CalculateAngle(target);
            float ticks = Quaternion.Angle(transform.GetChild(1).transform.rotation, targetRotation) / gunSpeed;
            InvokeRepeating("Shoot", ticks, cd);
        }
    }

    /// <summary>
    /// Stops firing
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !collision.CompareTag("Projectile") && collision.CompareTag("Player"))
        {
            CancelInvoke("Shoot");
            target = null;
            detected = false;
        }
    }

    /// <summary>
    /// Shoots bullet from gun`s spawnpoint
    /// </summary>
    private void Shoot()
    {
        var bullet = GameObject.Instantiate(bulletObj, spawnPoint.position, 
            gameObject.transform.GetChild(1).rotation, gameObject.transform);
        Destroy(bullet, 3);
    }

    /// <summary>
    /// Calculates angle from this object to target object
    /// </summary>
    private Quaternion CalculateAngle(GameObject target)
    {
        Vector3 lookDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return targetRotation;
    }

    /// <summary>
    /// Rotates turret after player
    /// </summary>
    private void RotateAfter(GameObject target)
    {
        Quaternion targetRotation = CalculateAngle(target);
        transform.GetChild(1).transform.rotation =
            Quaternion.RotateTowards(transform.GetChild(1).transform.rotation, targetRotation,
            Time.deltaTime * gunSpeed);
    }
}
