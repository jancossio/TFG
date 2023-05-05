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
        remainingLives = PlayerPrefs.GetInt("lifes", 4);
        Debug.Log("This is my life: " + remainingLives);
        //PlayerPrefs.SetInt("lifes", remainingLives);
        for (int i=0; i< playerLives.Length; i++)
        {
            if (i+1 <= remainingLives)
            {
                playerLives[i].enabled = true;
            }
            else
            {
                playerLives[i].enabled = false;
            }
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
            PlayerPrefs.SetInt("lifes", remainingLives);
            playerLives[remainingLives].enabled = false;
            GameManager.Instance.StartGameOverScreen();
        }
        else
        {
            //Decreases the quantity of remainigLives by one
            remainingLives--;
            PlayerPrefs.SetInt("lifes", remainingLives);
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
        remainingLives = Mathf.Clamp(remainingLives, 0, 4);
        playerLives[remainingLives-1].enabled = true;
        PlayerPrefs.SetInt("lifes", remainingLives);
        Debug.Log("Recoverevrv " + remainingLives);
    }

    public bool CheckMissingLifes()
    {
        return (remainingLives < 4);
    }

    public void ResetLives()
    {
        remainingLives = 4;
        PlayerPrefs.SetInt("lifes", 4);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RecoverLife();
        }
    }
}
