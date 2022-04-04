using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    private bool isGrounded;
    private bool isJumping;
    public bool isDead;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool invincible;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform foot;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public SpriteRenderer sprite;

    private Rigidbody2D rb;

    public static PlayerMovement instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(foot.position, 0.1f, ground);
        playerAnimator.SetBool("ground", isGrounded);
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDead)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            AudioManager.instance.audioPlay("jump");
        }

        //High Jump
        if (Input.GetKey(KeyCode.Space) && isJumping && !isDead)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        //Release Jump Key
        if (Input.GetKeyUp(KeyCode.Space) && !isDead)
        {
            isJumping = false;
        }

        //Speed power up
        if (Input.GetKeyDown(KeyCode.E) && !PowerUp.instance.isPowerup)
        {
            PowerUp.instance.SpeedPowerUp();
        }

        //Giant power up
        if (Input.GetKeyDown(KeyCode.R) && !PowerUp.instance.isPowerup)
        {
            PowerUp.instance.GiantPowerUp();
        }

        //Giant power up
        if (Input.GetKeyDown(KeyCode.T))
        {
            PowerUp.instance.HpPowerUp();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            PowerUp.instance.SlowTimePowerUp();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle" && !invincible)
        {
            AudioManager.instance.audioPlay("hit");
            HealthManager.instance.health--;
            invincible = true;
            StartCoroutine(FlashAfterDamage());
            if(HealthManager.instance.health < 1)
            {
                AudioManager.instance.audioPlay("dead");
            }
        }
    }

    IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.08f;
        for(int i  = 0; i < 10; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            sprite.enabled = true;
            yield return new WaitForSeconds(flashDelay);
        }
        if (!PowerUp.instance.isPowerup)
        {
            invincible = false;
        }
        
    }

    public void isDeaded()
    {
        isDead = true;
        StartCoroutine(GameManager.instance.ShowEndingPanel());
        playerAnimator.SetBool("dead", isDead);
    }

}
