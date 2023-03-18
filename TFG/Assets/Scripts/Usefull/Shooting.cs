using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectileItem;
    public Transform throwPoint;
    public bool canShoot = true;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(!canShoot)
        {
            return;
        }
        else
        {
            GameObject ob = Instantiate(projectileItem, throwPoint);
            ob.transform.parent = null;
        }
    }
}
