using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[Header("BasicPlayer")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform groundCheckCollider;
    public Transform overHeadCheckCollider;
    public LayerMask groundLayer;
    public Collider2D standingCollider, crouchingCollider;

    public Transform wallCheck;
    public LayerMask wallLayer;
    public bool isWallSliding;
    private float wallSlideSpeed = 2f;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    public int totalJumps;
    int availableJumps;
    float horizontalValue;
    float runSpeedModifier = 1.5f;
    float crouchSpeedModifier = 0.5f;

    public float speed;
    public float jumpPower = 250;
    bool facingRight = true;
    bool isRunning = false;
    bool isGrounded = false;
    bool crouchPressed = false;
    bool multipleJump;
    bool coyoteJump;
    bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        availableJumps = totalJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {
            return;
        }

        if (!CanMove())
            return;
        //Set yVelocity in animator
        animator.SetFloat("yVelocity", rb.velocity.y);

        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        //If jump botton is pressed the jump action is enabled
        if (Input.GetButtonDown("Jump") && !crouchPressed){
            Jump();
        }

        //If crouch botton is pressed the jump action is enabled
        if (Input.GetButtonDown("Crouch")){
            crouchPressed = true;
        }else if (Input.GetButtonUp("Crouch")){
            crouchPressed = false;
        }

        WallSlide();
    }

    private void FixedUpdate()
    {
        Move(horizontalValue, crouchPressed);
        GroundCheck();
    }

    bool CanMove()
    {
        bool able = true;
        if (FindObjectOfType<InteractionSystem>().isAnalyzing)
        {
            able = false;
            horizontalValue = 0f;
        }
        
        return able;
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Checks if "GroundCheckCollider" is colliding with other objects in the Ground Layer
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }
        }
        else
        {
            if (wasGrounded)
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
        //While the character is grounded, the Jump bool is disabled
        animator.SetBool("Jump", !isGrounded);
    }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        //if player is grounded and the jump button is pressed it jumps
        if (isGrounded)
        {
        multipleJump = true;
        availableJumps--;

        rb.velocity = Vector2.up * jumpPower;
        animator.SetBool("Jump", true);
            
        }
        else
        {
            if (multipleJump && availableJumps>0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

            if (coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }

    void Move(float dir, bool crouchFlag)
    {
        #region Crouch

        if (!crouchFlag){
            if (Physics2D.OverlapCircle(overHeadCheckCollider.position, overheadCheckRadius, groundLayer)){
                crouchFlag = true;
            }
        }

        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;

        #endregion

        #region Move&Run
        //Setting X value using dir & speed

        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        if (isRunning)
        {
            xVal *= runSpeedModifier;
        }

        if (crouchFlag)
        {
            xVal *= crouchSpeedModifier;
        }

        //Create new Vec2 for velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set player new Velocity
        rb.velocity = targetVelocity;

        //Store current scale value
        Vector3 currentScale = transform.localScale;
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1,1,1);
            facingRight = true;
        }
        //Idle: 0, Walk: 5, Run: 7.5
        //Debug.Log(rb.velocity.x);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded)
        {
            if (!isWallSliding)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));

            if (Input.GetButtonDown("Jump"))
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            isWallSliding = false;
        }
    }

    public void Die()
    {
        playerDead = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(overHeadCheckCollider.position, overheadCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(wallCheck.position, overheadCheckRadius);
    }
}
