using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : Enemy
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float distanceChase;
    [SerializeField] private Transform directionChecker;

    private float dir;
    private bool wallChecking;
    private bool cornerChecking;
    public float startTimer = 1.5f;
    private float Timer;

    // Start is called before the first frame update
    void Awake()
    {
        dir = 1f;
        Timer = startTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            //Debug.Log("I know you exist");
            if (Vector2.Distance(transform.position, target.position) < distanceChase)
            {
                Chase(target);
            }
            else
            {
                Patrol();
            }
        }
    }

    private void Chase(Transform target)
    {
        //dir = 1f;
        float dirTemp = transform.position.x - target.position.x;
        if (dirTemp > 0.3f)
        {
            dir = -1f;
        }
        else if (dirTemp < -0.3f)
        {
            dir = 1;
        }
        RaycastHit2D ray = Physics2D.Raycast(directionChecker.position, Vector2.down, 7f);
        if (ray.collider)
        {
            CheckDirection(dir);
            float xVal = dir * moveSpeed * 100 * Time.fixedDeltaTime;
            Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
            rb.velocity = targetVelocity;
            anim.SetBool("Idle", false);
        }
    }

    public void Patrol()
    {
        dir = 1f;

        RaycastHit2D ray = Physics2D.Raycast(directionChecker.position, Vector2.down, 1f);
        if (!ray.collider)
        {
            cornerChecking = true;
        }
        else
        {
            cornerChecking = false;
        }

        if (cornerChecking || wallChecking)
        {
            anim.SetBool("Idle", true);
            dir = 0f;
            if (Timer <= 0f)
            {
                Timer = startTimer;
                Flip();
            }
            else
            {
                Timer -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("Idle", false);
            if (!facingRight)
            {
                dir = -dir;
            }
        }

        float xVal = dir * moveSpeed * 100 * Time.fixedDeltaTime;
        Vector2 rbVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = rbVelocity;
    }

    public void SetWallChecker(bool isWalled)
    {
        wallChecking = isWalled;
    }
}
