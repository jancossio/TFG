using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private float enemyHealth;
    protected private SpriteRenderer spriteRend;
    [SerializeField] private GameObject destroyParticle;

    public Transform target;
    public Rigidbody2D rb;
    public bool facingRight = true;
    protected private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckDirection(float dir)
    {
        if (dir > 0)
        {
            if (!facingRight)
            {
                //Debug.Log("Flipped to right.");
                Flip();
            }

        }
        else if (dir < 0)
        {
            if (facingRight)
            {
                //Debug.Log("Flipped to left.");
                Flip();
            }

        }
    }

    public void Flip()
    {
       //Vector3 flipVec = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (facingRight)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        facingRight = !facingRight;
    }

    public void TakeDamage()
    {
        enemyHealth--;
        anim.Play("Hit");
        checkHealth();
    }

    public void checkHealth()
    {
        if (enemyHealth <= 0)
        {
            destroyParticle.SetActive(true);
            spriteRend.enabled = false;
            Invoke("deathDestruction", 0.4f);
        }
    }

    public void deathDestruction()
    {
        Destroy(transform.gameObject);
    }
}
