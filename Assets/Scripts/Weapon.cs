using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public Transform meleePoint;
    public GameObject fireballPrefab;
    public GameObject daggerPrefab;
    public GameObject arrowPrefab;
    private PlayerInput playerInput;
    public GameObject firepointObj;
    public GameObject player;
    public int weapon;
    private Animator anim;
    public float meleeRange;
    public LayerMask playerLayerMask;
    private double cooldownTime = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("weapon", weapon);
    }

    void OnShoot(InputValue value)
    {
        Shoot();
    }

    //Handles all weapon attacks
    private void Shoot()
    {
        //If cooldown is over and user is not dashing, allow them to shoot
        if (Time.time > cooldownTime && !playerInput.actions["Dash"].IsPressed()
            && !player.GetComponent<PlayerController>().IsBlocking() && weapon != 0)
        {
            print("Shooting now, on cooldown!");
            anim.SetTrigger("attack");

            //Fireball
            if (weapon == 4)
            {
                SoundManager.PlaySound("fireball");
                GameObject shot = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
                shot.GetComponent<Shot>().player = player;

                cooldownTime = Time.time + 0.7;
            }

            //Dagger
            else if (weapon == 3)
            {
                SoundManager.PlaySound("knife");
                GameObject shot = Instantiate(daggerPrefab, firePoint.position, firePoint.rotation);
                shot.GetComponent<Shot>().player = player;

                cooldownTime = Time.time + 0.4;
            }

            //Bow & Arrow
            else if (weapon == 2)
            {
                SoundManager.PlaySound("bow");
                GameObject shot = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
                shot.GetComponent<Shot>().player = player;

                cooldownTime = Time.time + 0.6;
            }

            //Lightsaber
            else if (weapon == 1)
            {
                SoundManager.PlaySound("lightsaber");
                Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, playerLayerMask);
                foreach (Collider2D hitPlayer in hitPlayers)
                {
                    //If hit player is not myself xD and hit player is not blocking
                    if (hitPlayer.gameObject != player && !hitPlayer.gameObject.GetComponent<PlayerController>().IsBlocking())
                    {
                        //Instakill player when hit with lightsaber
                        hitPlayer.gameObject.GetComponent<Health>().takeDamage(5);
                    }
                    //Knockback attacker if defender is blocking
                    else if(hitPlayer.gameObject.GetComponent<PlayerController>().IsBlocking())
                    {
                        SoundManager.PlaySound("shield");
                        GetComponent<Rigidbody2D>().AddForce((-transform.right + Vector3.up) * 5, ForceMode2D.Impulse);
                    }
                }

                cooldownTime = Time.time + 1.0;
            }
        }
        else
        {
            Debug.Log("Cannot shoot right now.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (meleePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
