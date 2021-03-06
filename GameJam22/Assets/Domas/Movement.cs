using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public GroundCheck groundCheck;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float jumpHeight = 1f;
    public float horizontalSpeed = 1f;
    public float cancelRate = 100;
    public float buttonTime = 0.5f;
    float jumpTime;
    bool jumping;
    bool jumpCancelled;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalTranslation = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;


        transform.Translate(new Vector3(horizontalTranslation, 0, 0));
        if (horizontalTranslation > 0 && transform.localScale.x < 0 || horizontalTranslation < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);

        if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (Input.GetButtonUp("Jump"))
            {
                jumpCancelled = true;
            }

            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
        animator.SetBool("isRunning", horizontalTranslation != 0);
        animator.SetBool("isJumping", jumping);

    }
    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
            animator.SetBool("isFalling", true);

        if (jumpCancelled && jumping && rb.velocity.y > 0) /*cancel jump*/
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
        else if (groundCheck.isGrounded && rb.velocity.y <= 0) /*snap to ground when is falling*/
        {
            animator.SetBool("isFalling", false);
            rb.velocity = new Vector2(rb.velocity.x, 0);

            // transform.position = new Vector3(groundCheck.surfacePosition.x, groundCheck.surfacePosition.y, transform.position.z);
        }

    }
}
