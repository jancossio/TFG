using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConceteBossGround : MonoBehaviour
{
    private Boss body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            body.SetGroundChecker(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            body.SetGroundChecker(false);
    }
}
