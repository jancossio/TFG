using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                //Debug.Log("This is supplyween: " + enemy);
                enemy.TakeDamage();
            }
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            BoxSupply supply = collision.GetComponentInParent<BoxSupply>();
            if (supply != null)
            {
                //Debug.Log("This is supplyween: " + supply);
                supply.loseHealthAtHit();
            }
        }
        //Debug.Log("I shot a dude: " + collision.gameObject.name);
        AudioManager.Instance.PlaySoundEffect("BulletBreak");
        Destroy(gameObject);
    }
}
