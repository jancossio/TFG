using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPrimary : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform[] patrolSpots;
    public SpriteRenderer spriteRend;
    public Animator anim;

    public float startTimer = 2f;
    private float Timer;
    private bool facingRight = false;

    private int point = 0;
    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        Timer = startTimer;
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckDirection());

        transform.position = Vector2.MoveTowards(transform.position, patrolSpots[point].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolSpots[point].position)<0.1f)
        {
            if (Timer <= 0f)
            {
                if (patrolSpots[point] != patrolSpots[patrolSpots.Length-1])
                {
                    point++;
                }
                else
                {
                    point = 0;
                }

                Timer = startTimer;
            }
            else
            {
                Timer -= Time.deltaTime;
            }
        }
    }

    IEnumerator CheckDirection()
    {
        pos = transform.position;
        yield return new WaitForSeconds(1f);

        if(transform.position.x > pos.x)
        {
            //spriteRend.flipX = true;
            anim.SetBool("Idle", false);
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else if(transform.position.x < pos.x){
            //spriteRend.flipX = false;
            anim.SetBool("Idle", false);
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if(transform.position.x == pos.x)
        {
            anim.SetBool("Idle", true);
        }
    }
}
