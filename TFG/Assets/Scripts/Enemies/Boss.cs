using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public enum stateType : int { idle = 0, charge = 1, shoot = 2, jump = 3, punchRain = 4 };
    stateType actualState, previousState;

    [SerializeField] private float chargeSpeed = 9f;
    private float targetDistance;
    [SerializeField] private bool isWalled = false;
    [SerializeField] private bool isGrounded = false;
    private float dir;

    private float attackTimer, stateTimer;
    [SerializeField] private float startTimer = 1f;
    [SerializeField] private float startStateTimer = 3.5f;
    private int nShots;
    private int nJumps;
    private int nBlocks;
    public Vector2 jumpTarget;
    [SerializeField] private float jumpSpeed = 10f;
    private float dirCharge;
    private bool chargeFixated = false;
    bool landedJump = false;
    float rbConst;

    [SerializeField] private Collider2D weakPoint;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Transform rainPoint;
    [SerializeField] private GameObject concreteProjectile;
    [SerializeField] private GameObject concreteBlock;

    // Start is called before the first frame update
    void Awake()
    {
        dir = -1f;
        attackTimer = startTimer;
        stateTimer = startStateTimer;
        actualState = stateType.idle;
        nShots = 3;
        dirCharge = 0;
        nJumps = 3;
        nBlocks = 6;
        jumpTarget = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        switch (actualState)
        {
            case stateType.idle:
                Idle();
                break;
            case stateType.charge:
                Charge();
                break;
            case stateType.shoot:                             
                Shoot();
                break;
            case stateType.jump:
                Jump();
                break;
            case stateType.punchRain:
                PunchRain();
                break;
        }
    }

    private void Charge()
    {
        if (!chargeFixated)
        {
            dirCharge = TargetCharge();
            CheckDirection(dirCharge);
        }

        if (!isWalled)
        {
            anim.Play("Charge");
            float xVal = dirCharge * chargeSpeed * 100 * Time.fixedDeltaTime;
            Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
            rb.velocity = targetVelocity;
            //Debug.Log("oooeoeeeeoeoe"+targetVelocity);
            //Debug.Log("Should be here: " + xVal);
        }
        else
        {
            if (attackTimer <= 3)
            {
                attackTimer = startTimer;
                Flip();
                chargeFixated = false;
                dirCharge = 0;
                //Invoke("ChangeState", 3f);
                //actualState = stateType.shoot;
                ChangeState();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

        }
    }

    private void Idle()
    {
        anim.Play("Idle");
        //Invoke("ChangeState", 3f);
        ChangeState();
    }

    private void PunchRain()
    {
        anim.Play("PunchRain");
        LookAtPlayer();
        if (nBlocks > 0)
        {
            if (attackTimer <= 3)
            {
                attackTimer = startTimer;
                DropBlock();
                nBlocks--;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (attackTimer <= 0)
            {
                attackTimer = startTimer;
                nBlocks = 6;
                ChangeState();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        if (nShots > 0)
        {
            if (attackTimer <= 0)
            {
                attackTimer = startTimer;
                anim.Play("Shot");
                Debug.Log(attackTimer);
                ShotProjectile();
                nShots--;

            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
            LookAtPlayer();
        }
        else
        {
            if (attackTimer <= 0)
            {
                attackTimer = startTimer;
                nShots = 3;
                //Invoke("ChangeState", 3f);
                //actualState = stateType.shoot;
                ChangeState();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        if (nJumps > 0)
        {
            if (!landedJump)
            {
                anim.Play("Jump");
                if (jumpTarget == Vector2.zero)
                {
                    rbConst = rb.gravityScale;
                    rb.gravityScale = 0f;
                    jumpTarget = new Vector2(target.position.x, transform.position.y + 5f);
                }

                transform.position = Vector2.MoveTowards(transform.position, jumpTarget, jumpSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, jumpTarget) < 0.1f)
                {
                    rb.gravityScale = rbConst;
                    landedJump = true;
                }
                LookAtPlayer();
            }
            else
            {
                if (isGrounded && landedJump)
                {
                    anim.Play("Landed");
                    if (attackTimer <= 0)
                    {
                        nJumps--;
                        attackTimer = startTimer;
                        jumpTarget = Vector2.zero;
                        landedJump = false;
                    }
                    else
                    {
                        attackTimer -= Time.deltaTime;
                    }
                }
            }
        }
        else
        {

            if (attackTimer <= 2)
            {
                attackTimer = startTimer;
                nJumps = 3;
                ChangeState();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

        }
    }

    private void ShotProjectile()
    {
        GameObject obj = Instantiate(concreteProjectile, shotPoint.position, Quaternion.identity) as GameObject;
        Vector2 firstShot = new Vector2(1 * dir, 0);
        obj.GetComponent<ConcreteBullet>().SetDirection(firstShot);
        obj.transform.parent = null;

        GameObject objTwo = Instantiate(concreteProjectile, shotPoint.position, Quaternion.identity) as GameObject;
        Vector2 secondShot = new Vector2(1 * dir, 1);
        objTwo.GetComponent<ConcreteBullet>().SetDirection(secondShot);
        objTwo.transform.parent = null;

        GameObject objThree = Instantiate(concreteProjectile, shotPoint.position, Quaternion.identity) as GameObject;
        Vector2 thirdShot = new Vector2(0, 1);
        objThree.GetComponent<ConcreteBullet>().SetDirection(thirdShot);
        objThree.transform.parent = null;
    }

    private void DropBlock()
    {
        float randX = Random.Range(rainPoint.position.x - 9.5f, rainPoint.position.x + 9.5f);
        Vector3 rainPos = new Vector3(randX, rainPoint.position.y, 0);
        GameObject obj = Instantiate(concreteBlock, rainPos, Quaternion.identity) as GameObject;
        obj.transform.parent = null;
    }

    private float TargetCharge()
    {
        float direction = 1f;
        if (transform.position.x > target.position.x)
        {
            direction = -1f;
        }
        chargeFixated = true;
        return direction;
    }

    public void SetWallChecker(bool wallHit)
    {
        if (wallHit)
        {
            isWalled = true;
            anim.SetBool("WallHit", true);
        }
        else
        {
            isWalled = false;
            anim.SetBool("WallHit", false);
        }
    }

    public void SetGroundChecker(bool groundHit)
    {
        if (groundHit)
        {
            isGrounded = true;
            anim.SetBool("WallHit", true);
        }
        else
        {
            isGrounded = false;
            anim.SetBool("WallHit", false);
        }
    }

    public void LookAtPlayer()
    {
        dir = 1f;
        if (transform.position.x > target.position.x)
        {
            dir = -1f;
        }
        CheckDirection(dir);
    }

    public void DamageBoss()
    {
        TakeDamage();
        if (!playerDamaged)
        {
            weakPoint.isTrigger = true;
            StartCoroutine(FlashHurtDelay());
        }
    }

    IEnumerator FlashHurtDelay()
    {
        yield return new WaitForSeconds(0.6f);
        float flashDelay = 0.0833f;
        for (int i = 0; i < 10; i++)
        {
            spriteRend.enabled = false;
            Debug.Log("Clear: " + spriteRend);
            yield return new WaitForSeconds(flashDelay);
            spriteRend.enabled = true;
            Debug.Log("White: " + spriteRend);
            yield return new WaitForSeconds(flashDelay);
        }
        weakPoint.isTrigger = false;
    }

    public void ChangeState()
   {
        //actualState = newState;
        if(actualState == stateType.idle)
        {
            if (Vector2.Distance(transform.position, target.position) > 8f)
            {
                actualState = stateType.shoot;
                Debug.Log("Now is charging");
            }
            else
            {
                actualState = stateType.jump;
                Debug.Log("Now is jumping");
            }
        }
        else if (actualState == stateType.charge)
        {
            if (Vector2.Distance(transform.position, target.position) > 6f)
            {
                actualState = stateType.jump;
                Debug.Log("Now is jumping");
            }
            else
            {
                actualState = stateType.punchRain;
                Debug.Log("Now is shooting");
            }
        }
        else if (actualState == stateType.shoot)
        {
            if (Vector2.Distance(transform.position, target.position) > 6f)
            {
                actualState = stateType.charge;
            }
            else
            {
                actualState = stateType.jump;
            }
        }
        else if (actualState == stateType.jump)
        {
            if (Vector2.Distance(transform.position, target.position) > 6f)
            {
                actualState = stateType.charge;
            }
            else
            {
                actualState = stateType.shoot;
            }
        }
        else if (actualState == stateType.punchRain)
        {
            actualState = stateType.idle;
        }
    }
}
