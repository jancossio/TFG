﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider2D collid;
    [SerializeField] private string enemyName;
    [SerializeField] protected private float enemyHealth;
    protected private SpriteRenderer spriteRend;
    [SerializeField] private GameObject destroyParticle;
    public bool canMove = true;
    protected private bool playerDamaged = true;

    #region Rooms
    public bool isBoss = false;
    public Vector3 newMinCoords, newMaxCoords;
    #endregion Rooms

    public Transform target;
    public Rigidbody2D rb;
    public bool facingRight = true;
    protected private Animator anim;

    public GameObject[] rewards = null;

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
        CheckCanHurt();
    }

    public void CheckDirection(float dir)
    {
        if (dir > 0)
        {
            if (!facingRight)
            {
                Flip();
            }

        }
        else if (dir < 0)
        {
            if (facingRight)
            {
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
        if (!playerDamaged)
        {
            //Debug.Log("Player taking damage: "+anim.name);
            enemyHealth--;
            AudioManager.Instance.PlaySoundEffect("EnemyHit");
            anim.Play("Hit");
            checkHealth();
        }
    }

    public void checkHealth()
    {
        if (enemyHealth <= 0)
        {
            SpawnReward();
            destroyParticle.SetActive(true);
            spriteRend.enabled = false;
            collid.isTrigger = true;
            AudioManager.Instance.PlaySoundEffect("DefeatExplosion");
            if (isBoss)
            {
                AudioManager.Instance.PlaySoundEffect("BossVictory");
                AudioManager.Instance.PlayMusicTrack("LevelSong");
                Invoke("GiveNewCoord", 0.5f);
                Invoke("deathDestruction", 1.5f);
            }
            else
            {
                Invoke("deathDestruction", 0.4f);
            }
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

        int nReward = Random.Range(0, rewards.Length-1);

        GameObject obj = Instantiate(rewards[nReward], transform.position, Quaternion.identity) as GameObject;
        obj.transform.parent = null;
    }

    private void CheckCanHurt()
    {
        if (target != null)
        {
            playerDamaged = target.GetComponent<PlayerMovement>().isInvincible;
        }
    }

    private void GiveNewCoord()
    {
        GameObject PlayerCamera = FindObjectOfType<CameraFollow>().gameObject;
        PlayerCamera.GetComponent<CameraFollow>().SetNewCheckpointBounds(newMinCoords, newMaxCoords);
    }
}
