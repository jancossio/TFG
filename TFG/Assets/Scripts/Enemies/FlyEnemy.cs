using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    [SerializeField] private float moveSpeed;
    public Transform[] patrolSpots;
    [SerializeField] private float attackDistance;
    private int point = 0;
    private bool finishedAttack = false;
    private Vector2 adquiredTarget;

    public float startTimer = 1.5f;
    private float attackTimer;

    // Start is called before the first frame update
    void Awake()
    {
        attackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, target.position) < attackDistance)
            {
                Chase();
            }
            else
            {
                Patrol();
            }
        }
    }

    private void Patrol()
    {

        transform.position = Vector2.MoveTowards(transform.position, patrolSpots[point].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolSpots[point].position) < 0.05f)
        {
            point++;

            if (point >= patrolSpots.Length)
            {
                point = 0;
            }
        }
        float dir = patrolSpots[point].position.x - transform.position.x;
        CheckDirection(dir);
    }

    private void Chase()
    {
        if(adquiredTarget == Vector2.zero)
        {
            adquiredTarget = target.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);


        if (Vector2.Distance(transform.position, patrolSpots[point].position) < 0.1f)
        {
            //finishedAttack = true;
            attackTimer = startTimer;
            adquiredTarget = Vector2.zero;
        }

            float dir = target.position.x - transform.position.x;
        CheckDirection(dir);
    }
}
