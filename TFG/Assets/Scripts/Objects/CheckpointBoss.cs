using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBoss : Checkpoint
{
    [SerializeField] private GameObject finalBoss;
    private float activateTimer;
    private Transform playerObj;
    [SerializeField] private float startTimer = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        activateTimer = startTimer;
        playerObj = GameObject.FindGameObjectWithTag("Player").transform;
        //finalBoss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (activateTimer <= 0)
            {
                //Debug.Log("Here's DaFinalBoss babyy!!");
                GameManager.Instance.SetRespawnPosition(playerObj);
                PlayerCamera.GetComponent<CameraFollow>().setBossActivation(true);
                finalBoss.SetActive(true);
                isTriggered = false;
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
                else
                {
                    //barrierManager(false);
                }
        }
    }
}
