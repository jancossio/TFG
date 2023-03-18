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

    public void LevelMenuScene()
    {
        PlayerPrefs.SetInt("lifes", 4);
        PlayerPrefs.SetFloat("healthbar", 100);
        PlayerPrefs.SetInt("actualLevel", 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("Level_0");
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
