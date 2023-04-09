using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform[] patrolSpots;

    public float startTimer = 2f;
    private float Timer;

    private int point = 0;
    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        Timer = startTimer;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, patrolSpots[point].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolSpots[point].position) < 0.1f)
        {
            if (Timer <= 0f)
            {
                if (patrolSpots[point] != patrolSpots[patrolSpots.Length - 1])
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
