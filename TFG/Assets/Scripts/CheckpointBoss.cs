using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBoss : Checkpoint
{
    [SerializeField] private GameObject finalBoss;
    private float activateTimer;
    [SerializeField] private float startTimer = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        activateTimer = startTimer;
        //finalBoss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (activateTimer <= 0)
            {
                Debug.Log("Here's DaFinalBoss babyy!!");
                finalBoss.SetActive(true);
            }
            else
            {
                activateTimer -= Time.deltaTime;
            }
        }
    }
}
