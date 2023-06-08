using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBoss : Checkpoint
{
    [SerializeField] private GameObject finalBoss;
    private float activateTimer;
    private Transform playerObj;
    private bool canAccess = true;
    [SerializeField] private float startTimer = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        activateTimer = startTimer;
        playerObj = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && canAccess)
        {
            if (activateTimer <= 0)
            {
                GameManager.Instance.SetRespawnPosition(playerObj);
                PlayerCamera.GetComponent<CameraFollow>().setBossActivation(true);
                finalBoss.SetActive(true);
                AudioManager.Instance.PlayMusicTrack("BossSong");
                canAccess = false;
            }
            else
            {
                activateTimer -= Time.deltaTime;
            }
        }

        if(playerObj == null)
        {
                GameObject result = GameObject.FindGameObjectWithTag("Player");
                if (result != null)
                {
                    playerObj = result.transform;
                }
        }
    }
}
