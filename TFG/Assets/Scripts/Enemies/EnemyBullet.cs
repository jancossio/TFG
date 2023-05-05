using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private GameObject player;
    private Vector2 direction;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        //Debug.Log("I'm shooting you 1: " + direction);
        //Debug.Log("I'm shooting you 2: " + -transform.localScale.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Yep, i've collisioned" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Yep, is the player");
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                //Debug.Log("This is supplyween: " + enemy);
                player.TakeDamage(20f);
            }
        }
        Destroy(gameObject);
    }
}
