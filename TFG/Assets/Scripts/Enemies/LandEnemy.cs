using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : Enemy
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceChase;
    [SerializeField] private Transform directionChecker;
    [SerializeField] private LayerMask groundMask;
    private float distanceChecker = 1f;

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
        if (Vector2.Distance(transform.position, target.position) < distanceChase)
        {
            Chase(target);
        }
        else
        {
            Patrol();
        }
    }

    private void Chase(Transform target)
    {
        dir = 1f;
        if (transform.position.x > target.position.x)
        {
            dir = -1f;
        }
        float xVal = dir * moveSpeed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;
        anim.SetBool("Idle", false);
        CheckDirection(dir);
    }

    public void Patrol()
    {
        dir = 1f;

        RaycastHit2D ray = Physics2D.Raycast(directionChecker.position, Vector2.down, distanceChecker, groundMask);
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
