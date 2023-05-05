using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] levelsButtons;

    private void Start()
    {
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        Debug.Log("Num Levels Unlock: " + levelsUnlocked);
        Debug.Log(" Level to Unlock: " + levelsButtons.Length);
        for (int i=0; i< levelsButtons.Length; i++)
        {
            //Debug.Log(levelsUnlocked);
            if (i+1 > levelsUnlocked)
            {
                levelsButtons[i].interactable = false;
            }
        }
    }

    public void SelectLevel(int sceneNumber)
    {
        PlayerPrefs.SetInt("actualLevel", sceneNumber);
        SceneManager.LoadScene(sceneNumber);
    }

  /*  #region Levels
    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelTutorialScene()
    {
        SceneManager.LoadScene("Level_0");
    }

    public void LevelOneScene()
    {
        PlayerPrefs.SetInt("actualLevel", 1);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelTwoScene()
    {
        PlayerPrefs.SetInt("actualLevel", 2);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelThreeScene()
    {
        PlayerPrefs.SetInt("actualLevel", 3);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelFourScene()
    {
        PlayerPrefs.SetInt("actualLevel", 4);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelFiveScene()
    {
        PlayerPrefs.SetInt("actualLevel", 5);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelSixScene()
    {
        PlayerPrefs.SetInt("actualLevel", 6);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelSevenScene()
    {
        PlayerPrefs.SetInt("actualLevel", 7);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelEightScene()
    {
        PlayerPrefs.SetInt("actualLevel", 8);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelNineScene()
    {
        PlayerPrefs.SetInt("actualLevel", 9);
        //SceneManager.LoadScene("Levels");
    }

    public void LevelTenScene()
    {
        PlayerPrefs.SetInt("actualLevel", 10);
        //SceneManager.LoadScene("Levels");
    }
    #endregion Levels

    #region Cinematics
    public void StartCinematic()
    {
        //SceneManager.LoadScene("MainMenu");
    }

    public void EndingCinematic()
    {
        //SceneManager.LoadScene("MainMenu");
    }
    #endregion Cinematics*/
}