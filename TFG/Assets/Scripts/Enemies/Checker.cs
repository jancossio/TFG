using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private LandEnemy body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<LandEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            body.Flip();
            body.SetWallChecker(true);
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
