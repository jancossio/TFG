using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public int damageDealt = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("aaaaaahh:");
            FindObjectOfType<PlayerMovement>().TakeDamage(damageDealt);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().TakeDamage(damageDealt);
        }
    }
}
