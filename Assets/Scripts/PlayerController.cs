using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    //Animation vars
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool facingRight;

    //Lateral movement vars
    private float moveValX;
    private Vector2 moveVector;
    private float moveSpeed;
    private bool isRunning;

    //Jumping vars
    public float jumpSpeed;
    private bool isGrounded;
    private bool isJumping;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask ground;
    private bool midair;

    //For double/triple jumping etc
    public int jumpAmount;
    private int jumpsLeft;

    //Shooting
    public Transform firePoint;
    private float firePointPosition;
    public float knockback;

    //Dash move
    public float dashSpeed;
    public float jogSpeed;
    private bool isDashing;

    //Item interaction
    private GameObject pickup;
    private bool inPickupRange;

    //Blocking attacks
    private bool isBlocking;
    public GameObject blockCircle;

    //Misc
    PlayerInput playerInput;
    Renderer rendererObj;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = jogSpeed;
        firePointPosition = firePoint.position.x;
        isGrounded = false;
        isJumping = false;
        midair = false;
        jumpsLeft = jumpAmount;
        rigidbody2d = GetComponent<Rigidbody2D>();
        facingRight = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rendererObj = GetComponent<Renderer>();
        isDashing = false;
    }

    //Executes when left stick is moved
    void OnMove(InputValue value)
    {
        //Get movement vector
        moveVector = value.Get<Vector2>();
        moveValX = System.Math.Abs(moveVector.x);
        if (moveValX > 0)
        {
            moveValX = 1;
        }
    }

    //Executes when A/South button is pressed
    void OnJump()
    {
        CheckSurroundings();
        if (isGrounded == true)
        {
            isJumping = true;
            isGrounded = false;
            jumpsLeft = jumpAmount;
        }
        else if (jumpsLeft > 0)
        {
            isJumping = true;
            isGrounded = false;
        }
    }

    //Checks if player is holding down the left bumper
    private void DashCheck()
    {
        CheckSurroundings();
        if (playerInput.actions["Dash"].IsPressed() && !midair && !isBlocking)
        {
            moveSpeed = dashSpeed;
            isDashing = true;
        }
        else
        {
            moveSpeed = jogSpeed;
            isDashing = false;
        }
    }

    //Checks if user is holding block button
    private void BlockCheck()
    {
        //If block button is being pressed
        if (playerInput.actions["Block"].IsPressed())
        {
            isBlocking = true;
            //Activate blocking circle
            blockCircle.SetActive(true);
        }
        else
        {
            isBlocking = false;
            //Deactivate blocking circle
            blockCircle.SetActive(false);
        }
    }

    void OnInteract()
    {
        if (inPickupRange)
        {
            if(pickup.CompareTag("RandomChest"))
            {
                //Set user weapon to whatever pickup contains
                GetComponent<Weapon>().weapon = pickup.GetComponent<Random_Weapon>().getContents();
                animator.ResetTrigger("attack");
                animator.SetTrigger("changeWeapon");
                //Play sound
                SoundManager.PlaySound("newItem");
            }
            else if(pickup.CompareTag("Pickup"))
            {
                //If health pickup
                if(pickup.GetComponent<General_Pickup>().getContents() == 5)
                {
                    //Restore player to full health
                    GetComponent<Health>().health = 5;
                    //Play sound
                    SoundManager.PlaySound("heal");
                }
            }
            //Despawn pickup
            Destroy(pickup);
        }
    }

    //Use for everything else, invoked once per frame
    void Update()
    {
        UpdateAnimations();
    }

    //Use for physics/rigidbodies (runs in lock-step with the physics engine)
    private void FixedUpdate()
    {
        DashCheck();
        BlockCheck();
        CheckDirection();
        if (isRunning)
        {
            transform.Translate(moveSpeed * Time.deltaTime * new Vector2(moveValX, 0));
        }

        if (isJumping == true && jumpsLeft > 0)
        {
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpsLeft--;
            isJumping = false;
            midair = true;
        }

        //Die if player falls offscreen
        if (!rendererObj.isVisible && rendererObj.transform.position.y < -6)
        {
            SoundManager.PlaySound("death");
            Destroy(gameObject);
        }
    }

    private void CheckDirection()
    {
        if (facingRight && moveVector.x < 0)
        {
            facingRight = !facingRight;
            rigidbody2d.transform.Rotate(0f, 180f, 0f);
        }
        else if (!facingRight && moveVector.x > 0)
        {
            facingRight = !facingRight;
            rigidbody2d.transform.Rotate(0f, 180f, 0f);
        }

        if (moveValX != 0 && !isBlocking)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isDashing", isDashing);
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        midair = !isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    //If player collider collides with a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If user enters weapon pickup/chest range
        if (collision.gameObject.CompareTag("Pickup") || collision.gameObject.CompareTag("RandomChest"))
        {
            print("in pickup range");
            inPickupRange = true;
            pickup = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If user exits weapon pickup/chest range
        if (collision.gameObject.CompareTag("Pickup"))
        {
            print("exited pickup range");
            inPickupRange = false;
            pickup = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Take 1 heart of damage if player touches spikes
        if(collision.gameObject.CompareTag("Spikes"))
        {
            GetComponent<Health>().takeDamage(1);
        }
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }
}