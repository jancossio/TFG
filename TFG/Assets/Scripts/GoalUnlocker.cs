using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUnlocker : MonoBehaviour
{
    int numUnlockedLevels;
    public int levelToUnlock;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            numUnlockedLevels = PlayerPrefs.GetInt("levelsUnlocked");
            if (numUnlockedLevels <= levelToUnlock)
            {
                PlayerPrefs.SetInt("levelsUnlocked", numUnlockedLevels+1);
            }
        }
    }
}
