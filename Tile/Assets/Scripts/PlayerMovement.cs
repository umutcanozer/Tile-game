using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PlayerMovement : MonoBehaviour
{
    //gravity scale 4
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 13f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float playerHealth = 10f;
    [SerializeField] float currentHealth;
    [SerializeField] Vector2 deathKick = new Vector2(0, 15f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;


    Vector2 moveInput;
    Rigidbody2D rgb2d;
    Animator anim;
    BoxCollider2D boxCollider;
    CapsuleCollider2D capsuleCollider;
    float defaultGravityScale;
    public bool isAlive = true;

    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        defaultGravityScale = rgb2d.gravityScale;
        currentHealth = playerHealth;
    }


    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {

        if (!isAlive) return;
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            rgb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, rgb2d.velocity.y);
        rgb2d.velocity = playerVelocity;

        bool isRunning = Mathf.Abs(rgb2d.velocity.x) > 0;
        anim.SetBool("isRunning", isRunning);
    }

    void ClimbLadder()
    {
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rgb2d.gravityScale = defaultGravityScale;
            anim.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(rgb2d.velocity.x, moveInput.y * climbSpeed);
        rgb2d.velocity = climbVelocity;
        rgb2d.gravityScale = 0;

        bool isClimbing = Mathf.Abs(rgb2d.velocity.y) > 0;
        anim.SetBool("isClimbing", isClimbing);


    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rgb2d.velocity.x) > Mathf.Epsilon;  // if the movement speed is greater than 0.00001 
        if (playerHasHorizontalSpeed) transform.localScale = new Vector2(Mathf.Sign(rgb2d.velocity.x), 1); //if the velocity equals to 0, it doesnt sign.
    }

    void Die()
    {

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            currentHealth -= 10f * Time.deltaTime;

            if(currentHealth <= 0)
            {
                currentHealth = playerHealth;
                FindObjectOfType<GameSession>().ProcesssPlayerDeath();
                isAlive = false;
                anim.SetTrigger("Dying");
                

                rgb2d.velocity = deathKick;
                boxCollider.isTrigger = true;
                capsuleCollider.isTrigger = true;                
            }         
        }    
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        if (value.isPressed) Instantiate(bullet, gun.position, transform.rotation);

    }
}




