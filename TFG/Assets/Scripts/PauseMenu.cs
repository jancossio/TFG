using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool pausedGame = false;
    public bool pauseIsAllowed = true;

    public GameObject pauseMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseIsAllowed)
        {
            if (pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
        pausedGame = false;
    }
    public void Pause()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
        pausedGame = true;
    }

    public void StopTime(bool stop)
    {
        if (stop)
        {
            Time.timeScale = 0f;
            pausedGame = true;
            AllowPause(false);
        }
        else
        {
            Time.timeScale = 1f;
            pausedGame = false;
            AllowPause(true);
        }
    }

    /*public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/

    /*public void Levels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }*/

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }*/

    /*public void Options()
    {
        Debug.Log("An options menu should be added here");
    }*/

    public void AllowPause(bool pause)
    {
        pauseIsAllowed = pause;
    }
}
