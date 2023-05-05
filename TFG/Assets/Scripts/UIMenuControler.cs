﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuControler : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Levels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance.gameOver)
        {
            GameManager.Instance.ResetStats();
        }
        SceneManager.LoadScene("MainMenu");
    }

    private void ResetPlayerStats()
    {
        FindObjectOfType<HealthBar>().RefillBar();
        FindObjectOfType<LifeCount>().ResetLives();
    }
}
