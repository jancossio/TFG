using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 camMinValues, camMaxValues;

    private GameObject PlayerCamera = null;


    private void Start()
    {
        PlayerCamera = FindObjectOfType<CameraFollow>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerRespawn>().ReachedCheckPoint(transform.position.x, transform.position.y);
            GameManager.Instance.SetRespawnPosition(transform);

            GetComponent<Animator>().SetBool("Taken", true);
            SetCameraBounds();
        }
    }

    private void SetCameraBounds()
    {
        PlayerCamera.GetComponent<CameraFollow>().SetNewCheckpointBounds(camMinValues, camMaxValues);
    }

    public void SetBounds(Vector3 minValues, Vector3 maxValues)
    {
        camMinValues = minValues;
        camMaxValues = maxValues;
    }

}
