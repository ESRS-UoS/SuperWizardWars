using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Destroy any projectiles that hit the blocking circle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
