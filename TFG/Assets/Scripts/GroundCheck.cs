using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int temp = collision.gameObject.layer;
        if (temp == 8 || temp == 10 || temp == 11)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int temp = collision.gameObject.layer;
        if (temp == 8 || temp == 10 || temp == 11)
        {
            isGrounded = false;
        }
    }
}
