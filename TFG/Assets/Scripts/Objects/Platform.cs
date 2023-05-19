using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;

    public float startTime;

    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    /*  void Update()
      {
          if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
          {
              if (Input.GetKeyDown(KeyCode.E))
              {
                  effector.rotationalOffset = 180f;
              }

              if (Input.GetKey(KeyCode.Space))
              {
                  setEffectorUp();
              }
          }
      }*/

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            waitTime = startTime;
        }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                setEffectorDown();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void setEffectorUp()
    {
        effector.rotationalOffset = 0;
    }

    public void setEffectorDown()
    {
        effector.rotationalOffset = 180f;
        StartCoroutine(PlatformDelay());
    }

    IEnumerator PlatformDelay()
    {
        yield return new WaitForSeconds(0.3f);
        setEffectorUp();
    }
}
