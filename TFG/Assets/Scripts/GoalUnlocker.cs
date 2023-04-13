using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalUnlocker : MonoBehaviour
{
    int numUnlockedLevels;
    public int levelToUnlock;

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
                PlayerPrefs.SetInt("levelsUnlocked", numUnlockedLevels+1);
            }
        }
    }
}
