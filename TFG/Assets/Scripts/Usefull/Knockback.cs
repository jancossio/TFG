using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust, knockTime, damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<Pot>().smash();
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    //hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    //other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                  /*  if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);*/
                    }

                }
                Vector2 diff = new Vector2(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y);
                diff = diff.normalized * thrust;
                hit.AddForce(diff, ForceMode2D.Impulse);
            }
    }
}
