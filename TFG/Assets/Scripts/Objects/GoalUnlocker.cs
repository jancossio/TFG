using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalUnlocker : MonoBehaviour
{
    int numUnlockedLevels;
    public int levelToUnlock;
    //[SerializeField] private string nextLevel;

    private void Start()
    {
        numUnlockedLevels = PlayerPrefs.GetInt("levelsUnlocked", 1);
        levelToUnlock = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (numUnlockedLevels < levelToUnlock)
            {
                Debug.Log("Num Levels Unlock: "+numUnlockedLevels);
                Debug.Log(" Level to Unlock: " + levelToUnlock);
                PlayerPrefs.SetInt("levelsUnlocked", numUnlockedLevels+1);
            }
            GameManager.Instance.StartScreenWin();
        }
    }

    /*public void SetParameters(int levelToAccess)
    {
        levelToUnlock = levelToAccess;
    }*/
}
