using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{

    private int cherriesTaken = 0;
    private int diamondsTaken = 0;
    private int numConePines = 0;

    /*public Text cherriesCollected;
    public Text diamondsCollected;
    public Text pinesAmmo;*/
    public TextMeshProUGUI cherriesCollected;
    public TextMeshProUGUI diamondsCollected;
    public TextMeshProUGUI pinesAmmo;

    public enum itemType : int { Cherry=0, Diamond=1, bandAid=2, aidKit=3, life=4 };

    itemType actualItem;

    private void Start()
    {
    cherriesTaken = PlayerPrefs.GetInt("cherries", 0);
    diamondsTaken = PlayerPrefs.GetInt("diamonds", 0);
    numConePines = PlayerPrefs.GetInt("pines", 0);
}

    private void Update()
    {
        cherriesCollected.text = cherriesTaken.ToString();
        diamondsCollected.text = diamondsTaken.ToString();
        pinesAmmo.text = numConePines.ToString();
    }

    public void itemObtained(int type)
    {
        switch(type)
        {
            case 0:
                cherriesTaken++;
                PlayerPrefs.SetInt("cherries", cherriesTaken);
                break;
            case 1:
                diamondsTaken++;
                PlayerPrefs.SetInt("diamonds", diamondsTaken);
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
                numConePines = Mathf.Clamp(numConePines, 0, 15);
                PlayerPrefs.SetInt("pines", numConePines);
                break;
            case 6:
                numConePines += 5;
                numConePines = Mathf.Clamp(numConePines, 0, 15);
                PlayerPrefs.SetInt("pines", numConePines);
                break;
            case 7:
                numConePines += 10;
                numConePines = Mathf.Clamp(numConePines, 0, 15);
                PlayerPrefs.SetInt("pines", numConePines);
                break;
        }
    }

    public int checkNumPines()
    {
        return numConePines;
    }

    public void shootPines()
    {
            numConePines--;
    }

    public void ResetItems()
    {
        PlayerPrefs.SetInt("cherries", 0);
        PlayerPrefs.SetInt("diamonds", 0);
        PlayerPrefs.SetInt("pines", 0);
    }
}
