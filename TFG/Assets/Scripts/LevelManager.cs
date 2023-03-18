using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //This function method will be called when the current level needs to be restarted (when the player dies, for example)
    public void Restart()
    {

        //First, the scene gets restarted
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Second, the players position gets restarted. Basically it gets reposited in the initial position

    }
}
