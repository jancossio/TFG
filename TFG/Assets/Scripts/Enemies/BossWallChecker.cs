using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallChecker : MonoBehaviour
{
    private Boss body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            body.SetWallChecker(true);
            //body.Flip();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            body.SetWallChecker(true);
            //body.Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            body.SetWallChecker(false);
        }
    }
}
