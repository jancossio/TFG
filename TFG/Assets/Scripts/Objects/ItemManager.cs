﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int numCherries = 0;
    public int numDiamonds = 0;

    private int cherriesTaken = 0;
    private int diamondsTaken = 0;
    private int numConePines = 0;

    bool cherriesDone = false, diamondsDone = false, allDone = false;

    public Text cherriesCollected;
    public Text diamondsCollected;
    public Text pinesAmmo;
    private int totalCherriesInLevel;

    public enum itemType : int { Cherry=0, Diamond=1, bandAid=2, aidKit=3, life=4 };

    itemType actualItem;

    private void Start()
    {
        totalCherriesInLevel = numCherries;
    }

    private void Update()
    {
        //itemCheck();
        cherriesCollected.text = cherriesTaken.ToString();
        diamondsCollected.text = diamondsTaken.ToString();
        pinesAmmo.text = numConePines.ToString();
    }

    public void itemCheck()
    {
        if(numCherries == 0 && !cherriesDone)
        {
            cherriesDone = true;
            Debug.Log("¡Bien!, has recogido todas las cerezas.");
        }

        if (numDiamonds == 0 && !diamondsDone)
        {
            diamondsDone = true;
            Debug.Log("¡Bien!, has recogido todos los diamantes.");
        }

        if (transform.childCount == 0 && !allDone)
        {
            allDone = true;
            Debug.Log("¡Bien!, has recogido todos los objetos.");
        }
    }

    public void itemObtained(int type)
    {
        switch(type)
        {
            case 0:
                numCherries--;
                cherriesTaken++;
                break;
            case 1:
                numDiamonds--;
                diamondsTaken++;
                break;
            case 2:
                FindObjectOfType<LifeCount>().RecoverLife();
                break;
            case 3:
                FindObjectOfType<HealthBar>().Heal(15f);
                break;
            case 4:
                FindObjectOfType<HealthBar>().Heal(50f);
                break;
            case 5:
                numConePines++;
                break;
            case 6:
                numConePines += 5;
                numConePines = Mathf.Clamp(numConePines, 0, 15);
                break;
            case 7:
                numConePines += 10;
                numConePines = Mathf.Clamp(numConePines, 0, 15);
                break;
        }
    }

    public int checkNumPines()
    {
        return numConePines;
        Debug.Log("Número de piñas insuficiente???");
    }

    public void shootPines()
    {
            numConePines--;
    }
}