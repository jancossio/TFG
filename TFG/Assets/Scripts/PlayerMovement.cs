using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public int currentHealth = 400;

    float horizontalValue;
    float verticalValue;

    public float speed = 2f;
    public float runSpeed = 4f;

    public float jumpPower;
    public float secondJump;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;
    public Collider2D standingCollider, crouchingCollider;

    public bool canJump = true;

    bool facingRight = true;
    public bool isGrounded;

    bool coyoteJump;
    bool multipleJump;

    int totalJumps = 2;
    int availableJumps;

    public bool isRunning = false;

    float runSpeedModifier = 1.5f;
    float crouchSpeedModifier = 0.5f;

    public bool crouchPressed = false;

    private float wallSlideSpeed = 2f;
    public bool isWallSliding;
    public float fallMultiplier = 0.5f;

    bool isClimbing = false;
    public LayerMask ladderLayer;

    bool isTakingDamage = false;
    bool isInvincible = false;
    public GameObject hitParticle;

    public Transform throwPoint;
    public float shotCadence;
    float shootTime;
    bool shotFlag = false;
    [SerializeField] GameObject bullet;
    //public bool traspassPlatform = false;

    // Start is called before the first frame update
    void Start()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("yVelocity", rb.velocity.y);

        //If crouch botton is pressed the jump action is enabled
        if (Input.GetButtonDown("Crouch")){
            crouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchPressed = false;
        }

        //If jump botton is pressed the jump action is enabled
        if (Input.GetButtonDown("Jump") && !crouchPressed){
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        WallSlide();

        if (Input.GetKeyDown(KeyCode.R))
            shotFlag = true;
        if (Input.GetKeyUp(KeyCode.R))
            shotFlag = false;
    }

    void FixedUpdate()
    {
        if (isTakingDamage)
        {
            anim.Play("Fox_hurt");
        }
        else
        {
            checkingGround();
            Climb();
            playerShoot();
            Move(horizontalValue, crouchPressed);
        }
    }

    void Move(float dir, bool crouchFlag)
    {
        #region Crouch

        if (!crouchFlag){
            if (CrouchCheck.isCrouched){
                crouchFlag = true;
            }
        }

        anim.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;

       /* if (GroundCheck.isGrounded && crouchFlag && Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Down now");
            //FindObjectOfType<Platform>().setEffectorDown();
            traspassPlatform = true;
        }
        traspassPlatform = false; */

        #endregion

        #region Move&Run
        //Setting X value using dir & speed

        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        if (isRunning){
            xVal *= runSpeedModifier;
        }

        if (crouchFlag){
            xVal *= crouchSpeedModifier;
        }

        //Create new Vec2 for velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set player new Velocity
        rb.velocity = targetVelocity;

        //Store current scale value
        Vector3 currentScale = transform.localScale;
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        //Idle: 0, Walk: 5, Run: 7.5
        //Debug.Log(rb.velocity.x);
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    void Jump()
    {
        //if player is grounded and the jump button is pressed it jumps
        if (GroundCheck.isGrounded && canJump)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            anim.SetBool("Jump", true);

            Debug.Log(" health now: " + 80 / 100);
        }
        else
        {
            if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * secondJump;
                Debug.Log(Vector2.up * jumpPower);
                anim.SetBool("Jump", true);
            }

            if (coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }
        }
    }

    private void WallSlide()
    {
        if (WallCheck.isWalled && !GroundCheck.isGrounded && canJump)
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
                canJump = false;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }
        }
        else
        {
            isWallSliding = false;
        }
    }

    void checkingGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        //Checks if "GroundCheckCollider" is colliding with other objects in the Ground Layer
        if (GroundCheck.isGrounded)
        {
            isGrounded = true;
            canJump = true;
            if (!wasGrounded){
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
        anim.SetBool("Jump", !isGrounded);
    }

    IEnumerator CoyoteJumpDelay()
    {
    coyoteJump = true;
    yield return new WaitForSeconds(0.2f);
    coyoteJump = false;
    }

    void Climb()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 3f, ladderLayer);
        //Physics2D.OverlapCircle(transform.position, 0.2f, ladderLayer);


        if (checkLadder())
        {
            //Debug.Log("Entered");
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                //Debug.Log("climbing");
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
            anim.SetBool("Climb", isClimbing);
        }
        
        if(isClimbing && checkLadder())
        {
            verticalValue = Input.GetAxisRaw("Vertical");
            rb.gravityScale = 0;
            rb.velocity = new Vector2(horizontalValue*speed, verticalValue * speed);
            anim.SetBool("Climb", isClimbing);
        }
        else
        {
            rb.gravityScale = 3;
        }
    }

    private bool checkLadder()
    {
        return Physics2D.OverlapCircle(transform.position, 0.2f, ladderLayer);
    }

    void playerShoot(){
        if (shotFlag)
        {

            shootTime += Time.deltaTime;

            if (shootTime >= shotCadence)
            {
                GameObject obj = Instantiate(bullet, throwPoint) as GameObject;
                obj.transform.parent = null;
                shootTime = 0;
            }
        }
        else
        {
            shootTime = shotCadence;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            //currentHealth -= damage;
            FindObjectOfType<HealthBar>().LoseHealth(damage);
            StartDamageAnimation();
            /*if (currentHealth <= 0)
            {
                //Kill player
            }
            else
            {
                StartDamageAnimation();
            }*/
        }
    }

    void StartDamageAnimation()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            isInvincible = true;
            float hitForceX = 2f * -transform.localScale.x;
            float hitForceY = 17f;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2 (hitForceX, hitForceY), ForceMode2D.Impulse);
            //StartCoroutine(HurtDelay());
            hitParticle.SetActive(true);
            rb.drag = 2;
            Invoke("StopDamageAnimation", 1f);
        }
    }

    public void StopDamageAnimation()
    {
        hitParticle.SetActive(false);
        rb.drag = 0;
        isTakingDamage = false;
        //anim.Play("Fox_hurt", -1, 0f);
        StartCoroutine(FlashHurtDelay());
    }

    IEnumerator FlashHurtDelay()
    {
        float flashDelay = 0.0833f;
        for (int i=0; i<10; i++)
        {
            sprite.color = Color.clear;
            yield return new WaitForSeconds(flashDelay);
            sprite.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }
        isInvincible = false;
    }
    /* IEnumerator HurtDelay()
     {
         rb.drag = 2;
         yield return new WaitForSeconds(0.5f);
         hitParticle.SetActive(false);
         yield return new WaitForSeconds(0.8f);
         StopDamageAnimation();
     }*/

    private void OnDrawGizmosSelected()
    {
        Vector3 nod = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
