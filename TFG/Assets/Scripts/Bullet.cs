using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 4f;

    public float damage = 10;
    public bool left;

    public float speed = 24f;
    public float destroyTimer;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }

        /*if (left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }*/
        transform.Translate(Vector3.right * transform.localScale.x * speed * Time.deltaTime);
    }

    public void Shoot()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

        }

        if (collision.gameObject.CompareTag("Box"))
        {
            BoxSupply supply = collision.GetComponentInParent<BoxSupply>();
            if(supply != null)
            {
                Debug.Log("This is supplyween: "+ supply);
                supply.loseHealthAtHit(damage);
            }
        }

        Destroy(gameObject);
    }
}
