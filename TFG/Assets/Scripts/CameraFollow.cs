using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 minValues, maxValues;

    float nextTimeToSearch = 0f;

    [Range(1, 10)] public float smoothFactor;
    Vector3 offset = new Vector3(0, 0, -10);

    private void FixedUpdate()
    {
        if (target != null)
        {
           Follow();
        }
        else
        {
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
    }

    public void SetNewCheckpointBounds(Vector3 nMinBounds, Vector3 nMaxBounds)
    {
        minValues = nMinBounds;
        maxValues = nMaxBounds;
    }
}
