using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider2D collid;
    [SerializeField] private string enemyName;
    [SerializeField] private float enemyHealth;
    protected private SpriteRenderer spriteRend;
    [SerializeField] private GameObject destroyParticle;
    public bool canMove = true;

    public Transform target;
    public Rigidbody2D rb;
    public bool facingRight = true;
    protected private Animator anim;
    [SerializeField] private int minChance = 0;
    public GameObject pine, band, first, hearth, cherrie;
    GameObject reward = null;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SearchForPlayer();
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
            SpawnReward();
            destroyParticle.SetActive(true);
            spriteRend.enabled = false;
            collid.isTrigger = true;
            Invoke("deathDestruction", 0.4f);
        }
    }

    public void deathDestruction()
    {
        Destroy(transform.gameObject);
    }

    void SearchForPlayer()
    {
        if(target == null)
        {
            GameObject result = GameObject.FindGameObjectWithTag("Player");
            if (result != null)
            {
                target = result.transform;
            }
        }
    }

    private void SpawnReward()
    {
       int nReward = Random.Range(minChance, 15);

       if(nReward <= 3)
       {
            reward = pine;
       }
       else if (nReward >= 4 && nReward <= 7)
       {
            reward = band;
       }
        else if (nReward >= 8 && nReward <= 11)
        {
            reward = cherrie;
        }
        else if (nReward >= 12 && nReward <= 13)
       {
            reward = first;
       }
       else if (nReward >= 14 && nReward <= 15)
       {
            reward = hearth;
       }

        GameObject obj = Instantiate(reward, transform.position, Quaternion.identity) as GameObject;
        obj.transform.parent = null;
    }
}
