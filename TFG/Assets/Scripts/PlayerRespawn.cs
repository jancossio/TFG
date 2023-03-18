using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private float checkpointPositionX, checkpointPositionY;

    // Start is called before the first frame update
    void Start()
    {
       if(PlayerPrefs.GetFloat("checkpointPositionX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("checkpointPositionX"), PlayerPrefs.GetFloat("checkpointPositionY"));
        }
    }

   public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkpointPositionX", x);
        PlayerPrefs.SetFloat("checkpointPositionY", y);
    }
}
