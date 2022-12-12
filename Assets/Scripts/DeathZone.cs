using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Destroys everything except ground and vision zones, inflicts death damage to player
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(9999);
            return;
        }
        if (collision.gameObject.tag != "Vision" && collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            Destroy(collision.gameObject);
    }
}
