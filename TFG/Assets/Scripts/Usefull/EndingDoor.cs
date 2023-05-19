using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingDoor : MonoBehaviour
{
    public Text text;
    public string levelName;
    private bool inDoor = false;

    int numUnlockedLevels;
    public int levelToUnlock;

    private void Start()
    {
        numUnlockedLevels = PlayerPrefs.GetInt("levelsUnlocked");
        levelToUnlock = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            text.gameObject.SetActive(true);
            inDoor = true;
            if (numUnlockedLevels <= levelToUnlock)
            {
                PlayerPrefs.SetInt("levelsUnlocked", numUnlockedLevels + 1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false);
        inDoor = true;
    }

    private void Update()
    {
        if(inDoor && Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public void SetParameters(Text UIText, string doorNextLevel)
    {
        text = UIText;
        levelName = doorNextLevel;
    }
}
