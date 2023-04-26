using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public static bool isWalled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.layer == 10)
        {
            isWalled = true;
        }*/
        isWalled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isWalled = false;
    }
}
