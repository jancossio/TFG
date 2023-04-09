using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float spriteWidth, startPosX, startPosY;
    public Transform cam;
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float controlDistance = cam.transform.position.x * (1 - parallaxSpeed);
        float distanceMoveX = cam.transform.position.x * parallaxSpeed;
        float distanceMoveY = cam.position.y * parallaxSpeed;

        transform.position = new Vector3(distanceMoveX + startPosX, distanceMoveY + startPosY, transform.position.z);

        if (controlDistance > startPosX+ spriteWidth)
        {
            startPosX += spriteWidth;
        }
        else if(controlDistance < startPosX - spriteWidth)
        {
            startPosX -= spriteWidth;
        }
    }
}
