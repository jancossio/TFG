using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalUnlocker : MonoBehaviour
{
    int numUnlockedLevels;
    [SerializeField] private int levelToUnlock;

    private void Start()
    {
        numUnlockedLevels = PlayerPrefs.GetInt("levelsUnlocked");
        levelToUnlock = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (numUnlockedLevels <= levelToUnlock)
            {
                Debug.Log("Num Levels Unlock: "+numUnlockedLevels);
                Debug.Log(" Level to Unlock: " + levelToUnlock);
                PlayerPrefs.SetInt("levelsUnlocked", numUnlockedLevels+1);
            }
            GameManager.Instance.StartScreenWin();
        }
    }
}
