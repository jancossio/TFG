using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 minValues, maxValues;
    public GameObject barriers;
    //public float minRespawnDistance = 8f;
    public float minYRespawnDistance = 8f;
    public float minXRespawnDistance = 13f;
    private bool barrierDown = false;
    private bool bossArena = false;

    [Range(1, 10)] public float smoothFactor;
    Vector3 offset = new Vector3(0,0,-10);

    private void FixedUpdate()
    {
        if (target != null)
        {
           Follow();
           if (barrierDown)
           {
                CheckRespawnDistance();
           }
        }
        else
        {
            Debug.Log("Where are you");
            if (!bossArena)
            {
                barrierManager(false);
            }
            SearchForPlayer();
            return;
        }
    }

    void Follow()
    {
        //Stablish minimum & maximum x, y, z values
        Vector3 targetPosition = target.position + offset;
        //Verify if targetPos is out of bounds
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
                                            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
                                            Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    void SearchForPlayer()
    {
        GameObject result = GameObject.FindGameObjectWithTag("Player");
        if (result != null)
        {
            target = result.transform;
        }
        else
        {
            //barrierManager(false);
        }
    }

    void CheckRespawnDistance()
    {

        if (Mathf.Abs(transform.position.x - target.position.x) < minXRespawnDistance)
        {
            if (Mathf.Abs(transform.position.y - target.position.y) < minYRespawnDistance)
            {
                barrierManager(true);
            }
        } 
    }

    void barrierManager(bool activate)
    {
        if (activate)
        {
            barriers.SetActive(true);
            barrierDown = false;
            //Debug.Log("BarrierIsUp");
        }
        else
        {
            barriers.SetActive(false);
            barrierDown = true;
            //Debug.Log("BarrierIsDown");
        }
    }

    public void SetNewCheckpointBounds(Vector3 nMinBounds, Vector3 nMaxBounds)
    {
        minValues = nMinBounds;
        maxValues = nMaxBounds;
    }

    public void setBossActivation(bool activated)
    {
        if (activated)
        {
            bossArena = true;
        }
        else
        {
            bossArena = false;
        }
    }
}
