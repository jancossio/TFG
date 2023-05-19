using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void LevelMenuScene()
    {
        PlayerPrefs.SetInt("lifes", 4);
        PlayerPrefs.SetFloat("healthbar", 100);
        PlayerPrefs.SetInt("actualLevel", 2);
        SceneManager.LoadScene("SampleScene");
    }*/

    public void PlayScene()
    {
        PlayerPrefs.SetInt("lifes", 4);
        PlayerPrefs.SetFloat("healthbar", 100);
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        PlayerPrefs.SetInt("NewGame", 1);
        int tempLastScene = 2;
        PlayerPrefs.SetInt("actualLevel", tempLastScene);
        //int tempLastScene = PlayerPrefs.GetInt("actualLevel", 2);
        SceneManager.LoadScene(tempLastScene);
    }

    public void LevelsScene()
    {
        int startedGame = PlayerPrefs.GetInt("NewGame", 0);
        if (startedGame >= 1)
        {
            SceneManager.LoadScene("Levels");
        }
    }

    public void TestScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
