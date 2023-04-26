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

    public void Levels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        Debug.Log("An options menu should be added here");
    }

    public void AllowPause(bool pause)
    {
        pauseIsAllowed = false;
    }
}
