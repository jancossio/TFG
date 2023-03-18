using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Collider2D collid;

    public Animator anim;

    public SpriteRenderer spriteRend;

    public GameObject destroyParticle;

    public float jumpForce = 8f;

    public int health = 2;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpForce);
            //Debug.Log("JumpDamage: " + collision.gameObject.GetComponent<Rigidbody2D>().velocity);
            getDamage();
            checkHealth();
        }
    }

    public void getDamage()
    {
        health--;
        //hitAnimation
    }

    public void checkHealth()
    {
        if(health == 0)
        {
            destroyParticle.SetActive(true);
            spriteRend.enabled = false;
            collid.isTrigger = true;
            Invoke("deathDestruction", 0.4f);
        }
    }

    public void deathDestruction()
    {
        Destroy(gameObject);
    }
}
