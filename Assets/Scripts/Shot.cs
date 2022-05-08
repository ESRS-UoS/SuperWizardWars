using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float shotSpeed;
    public Rigidbody2D shotRB;
    public float knockback;
    public GameObject player;
    private Renderer rendererObj;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        shotRB.AddRelativeForce(transform.right * shotSpeed);
        rendererObj = GetComponent<Renderer>();
    }

    //When shot collides with another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject != player)
        {
            collision.attachedRigidbody.AddForce((transform.right + Vector3.up) * knockback, ForceMode2D.Impulse);
            //Remove x hearts if hit
            collision.gameObject.GetComponent<Health>().takeDamage(damage);
            Debug.Log("Hit Player");
            Debug.Log(collision);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit Tile");
            Debug.Log(collision);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(rendererObj.isVisible == false)
        {
            Destroy(gameObject);
        }
    }
}