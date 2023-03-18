﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       /* if(collision.tag == "Player"){
            return;
        }*/

        if (collision.GetComponent<ShootingAction>())
        {
            collision.GetComponent<ShootingAction>().Action();
        }

        Destroy(gameObject);
    }
}
