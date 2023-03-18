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
        remainingLives = PlayerPrefs.GetInt("lifes");
        for(int i=0; i< remainingLives; i++)
        {
            playerLives[i].enabled = true;
        }
    }

    public void LoseLife()
    {
        remainingLives = PlayerPrefs.GetInt("lifes");
        //Debug.Log("LifeCount:LoseLife Hay " + remainingLives + " vidas");
        //If there aren't any remaining lives, is declared Game Over for the player
        if(remainingLives <= 0)
        {
            //FindObjectOfType<PlayerController>().Die();
            FindObjectOfType<LevelManager>().Restart();
        }

        //Decreases the quantity of remainigLives by one
        remainingLives--;
        PlayerPrefs.SetInt("lifes", remainingLives);
        //And, the last live on the vector is hidden
        //FindObjectOfType<LevelManager>().Restart();
        playerLives[remainingLives].enabled = false;
        FindObjectOfType<HealthBar>().RefillBar();

    }

    private void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("GAME");
            LoseLife();
        }*/
    }
}
