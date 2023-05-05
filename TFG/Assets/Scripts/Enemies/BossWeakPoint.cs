using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    public float jumpForce = 8f;

    private Boss bossEnemy;

    void Start()
    {
        bossEnemy = GetComponentInParent<Boss>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpForce);
            getDamage();
        }
    }

    public void getDamage()
    {
        bossEnemy.DamageBoss();
    }
}
