using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 minValues, maxValues;
    [Range(1, 10)] public float smoothFactor;
    Vector3 offset = new Vector3(0, 0, -10);

    private void FixedUpdate()
    {
        if (target != null)
        {
           Follow();
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
}
