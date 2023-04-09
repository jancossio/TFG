using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSupply : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spriteRend;

    public Collider2D collid;
    public GameObject boxCollider;

    //public GameObject breakEffect; for the future

    public float health = 1;
    public float jumpPower = 10f;
    public GameObject item;

    private void Start()
    {
        item.SetActive(false);
        item.transform.SetParent(FindObjectOfType<ItemManager>().transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpPower;
            loseHealthAtHit(10f);
        }
    }

    public void loseHealthAtHit(float damage)
    {
        health -= damage;
        //spriteRend.color = Color.black;
        anim.Play("boxHit");
        checkHealth();
    }

    public void checkHealth()
    {
        if(health <= 0)
        {
            item.SetActive(true);
            collid.enabled = false;
            boxCollider.SetActive(false);
            //break in parts for the future
            Invoke("boxDestroy", 0.5f);
        }
    }


    public void boxDestroy()
    {
        spriteRend.enabled = false;
        Destroy(transform.parent.gameObject);
    }
}
