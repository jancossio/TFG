using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] playerLives;
    int remainingLives;

    private void Start()
    {
        //remainingLives = PlayerPrefs.GetInt("lifes");
        remainingLives = 4;
        //PlayerPrefs.SetInt("lifes", remainingLives);
        for (int i=0; i< remainingLives; i++)
        {
            playerLives[i].enabled = true;
        }
    }

    public void LoseLife()
    {
        //remainingLives = PlayerPrefs.GetInt("lifes");

        //If there aren't any remaining lives, is declared Game Over for the player
        if(remainingLives <= 1)
        {
            //FindObjectOfType<LevelManager>().Restart();
            remainingLives--;
            playerLives[remainingLives].enabled = false;
            GameManager.Instance.StartScreenWin();
        }
        else
        {
            //Decreases the quantity of remainigLives by one
            remainingLives--;
            playerLives[remainingLives].enabled = false;
            FindObjectOfType<HealthBar>().RefillBar();
        }
        //PlayerPrefs.SetInt("lifes", remainingLives);
        //And, the last live on the vector is hidden
        Debug.Log("Vidas: "+remainingLives);

    }

    public void RecoverLife()
    {
        remainingLives++;
        playerLives[remainingLives-1].enabled = true;
    }

    public bool checkMissingLifes()
    {
        return (remainingLives < 4);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("GAME");
            LoseLife();
        }
    }
}
