using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int numCherries = 0;
    public int numDiamonds = 0;

    private int cherriesTaken = 0;
    private int diamondsTaken = 0;

    bool cherriesDone = false, diamondsDone = false, allDone = false;

    public Text cherriesCollected;
    public Text diamondsCollected;
    private int totalCherriesInLevel;

    public enum itemType : int { Cherry=0, Diamond=1 };

    itemType actualItem;

    private void Start()
    {
        totalCherriesInLevel = numCherries;
    }

    private void Update()
    {
        itemCheck();
        cherriesCollected.text = cherriesTaken.ToString();
        diamondsCollected.text = diamondsTaken.ToString();
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
        }
    }
}
