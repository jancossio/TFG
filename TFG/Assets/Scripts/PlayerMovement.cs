using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    //public int currentHealth = 400;

    float horizontalValue;
    float verticalValue;

    public float speed = 2f;

    public float jumpPower;
    public float secondJump;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;
    public Collider2D standingCollider, crouchingCollider;

    public bool canJump = true;

    bool facingRight = true;
    public bool isGrounded;

    public float fallMultiplier = 0.5f;
    bool coyoteJump;
    bool multipleJump;

    int totalJumps = 2;
    int availableJumps;

    public bool isRunning = false;

    float runSpeedModifier = 1.25f;
    float crouchSpeedModifier = 0.5f;

    public bool crouchPressed = false;

    public bool gradualJump = true;

    private float wallSlideSpeed = 2f;
    public bool isWallSliding;
    private bool isWallJumping = false;
    private float wallJumpDirection;
    private Vector2 wallJumpPower = new Vector2(7f, 14f);

    bool isClimbing = false;
    public LayerMask ladderLayer;

    bool isTakingDamage = false;
    bool isInvincible = false;
    public GameObject hitParticle;

    public Transform throwPoint;
    public float shotCadence;
    float shootTime;
    bool shotFlag = false;

    public bool canShoot = true;
    [SerializeField] GameObject bullet;

    public bool freezePlayer = false;
    public bool freezeControls = false;
    RigidbodyConstraints2D tempConstr;

    public AudioSource foxSource;

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

        horizontalValue = Input.GetAxis("Horizontal");

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

       /* if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        WallSlide();

        if (Input.GetMouseButtonDown(0))
            Shoot();
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
            //playerShoot();
            if (!isWallJumping)
            {
                Move(horizontalValue, crouchPressed);
            }
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
            rb.velocity = Vector2.up * jumpPower;
            anim.SetBool("Jump", true);

        }
        else
        {
            if (multipleJump)
            {
                rb.velocity = Vector2.up * secondJump;
                multipleJump = false;
                anim.SetBool("Jump", true);
            }

            if (coyoteJump)
            {
                multipleJump = true;

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
                //availableJumps = totalJumps;
                multipleJump = false;
            }
            isWallSliding = true;
            wallJumpDirection = -transform.localScale.x;

            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));

            if (Input.GetButtonDown("Jump"))
            {
                //availableJumps--;
                isWallJumping = true;
                //canJump = false;
                rb.velocity = new Vector2(wallJumpDirection*wallJumpPower.x, wallJumpPower.y);
                anim.SetBool("Jump", true);
                if (transform.localScale.x != wallJumpDirection)
                {
                    Vector3 tempScale = transform.localScale;
                    tempScale.x *= -1f;
                    facingRight = !facingRight;
                    transform.localScale = tempScale;
                }

                StartCoroutine(WallJumpDelay());
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
                //availableJumps = totalJumps;
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
    yield return new WaitForSeconds(0.25f);
    coyoteJump = false;
    }

    IEnumerator WallJumpDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isWallJumping = false;
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

    void Shoot()
    {
        if (canShoot)
        {
            int tempCheck = FindObjectOfType<ItemManager>().checkNumPines();
            if (tempCheck > 0)
            {
                FindObjectOfType<ItemManager>().shootPines();
                GameObject obj = Instantiate(bullet, throwPoint) as GameObject;
                obj.transform.parent = null;
                shootTime = 0;
                canShoot = false;
                StartCoroutine(shotDelay());
            }
        }
    }

    IEnumerator shotDelay()
    {
        yield return new WaitForSeconds(shotCadence);
        canShoot = true;
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            FindObjectOfType<HealthBar>().LoseHealth(damage);
            StartDamageAnimation();
        }
    }

    void StartDamageAnimation()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            isInvincible = true;
            float hitForceX = 6f * -transform.localScale.x;
            float hitForceY = 17f;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2 (hitForceX, hitForceY), ForceMode2D.Impulse);
            //StartCoroutine(HurtDelay());
            foxSource.Play();
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

    public void Kill()
    {
        FreezeRB(true);
        StartCoroutine(KillDelay());
    }

     public IEnumerator KillDelay()
     {
        anim.Play("Fox_hurt");
        yield return new WaitForSeconds(1.3f);
        //FreezePlayer(false);
        GameManager.Instance.KillPlayer(this);
     }

    public void FreezePlayer(bool freezeIt)
    {
        FreezeRB(freezeIt);
        freezeControls = freezeIt;
    }

    public void FreezeRB(bool freeze)
    {
        if (freeze)
        {
            freezePlayer = true;
            tempConstr = rb.constraints;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            freezePlayer = true;
            rb.constraints = tempConstr;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 nod = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
