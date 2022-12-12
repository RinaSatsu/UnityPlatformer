using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActive = false;

    /// <summary>
    /// Checkpoint activation
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            collision.gameObject.GetComponent<Player>().respawnPoint = gameObject.transform;
            gameObject.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            isActive = true;
        }
    }
}
