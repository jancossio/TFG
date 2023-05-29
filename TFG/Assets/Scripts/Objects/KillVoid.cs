using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVoid : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("aaaaaahh:");
            FindObjectOfType<PlayerMovement>().KillFallen();
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().TakeDamage(damageDealt);
        }
    }*/
}
