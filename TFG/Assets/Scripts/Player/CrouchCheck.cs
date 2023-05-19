using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchCheck : MonoBehaviour
{
    public static bool isCrouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int temp = collision.gameObject.layer;
        if (temp == 8 || temp == 10)
        {
            isCrouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCrouched = false;
    }
}
