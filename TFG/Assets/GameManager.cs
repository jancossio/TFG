using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public static GameManager gameManag;
    private static int lifes;
    private int actualLevel;
    private float health;
    private int points;

    public Transform playerCharacter;
    public Transform respawnPlayerPos;

    
    public bool gameOver = false;
    private bool canPauseGame = true;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject WinScreenUI;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        //StartPlayer();

        lifes = 4;
        health = 100f;
        actualLevel = 0;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    int getVidas()
    {
        return lifes;
    }

    public void incVida()
    {
        lifes += 1;
    }

    void incPoints(int num)
    {
        points += num;
    }

    int getPoints()
    {
        return points;
    }

    public void ResetStats()
    {
        lifes = 4;
        health = 100f;
        PlayerPrefs.SetInt("lifes", lifes);
        PlayerPrefs.SetFloat("healthbar", health);
        points = 0;
    }


    public void RespawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacter, respawnPlayerPos.position, respawnPlayerPos.rotation);
        AudioManager.Instance.PlaySoundEffect("RespawnPlayer");
        playerCharacter.GetComponent<PlayerMovement>().SetInvincibility(false);
    }

    public void KillPlayer(PlayerMovement player)
    {
        lifes = PlayerPrefs.GetInt("lifes", 0);
        if (lifes < 1)
        {
            Destroy(player.gameObject);
        }
        else
        {
            lifes--;
            Instance.RespawnPlayer();
            Destroy(player.gameObject);
        }
    }

    public void preparePlayerLevel(int nLevel)
    {
        int tempActualLevel = PlayerPrefs.GetInt("actualLevel");
        if(tempActualLevel != nLevel)
        {
            lifes = 4;
            points = 0;
            PlayerPrefs.SetFloat("healthbar", 100);
        }
    }

    public void StartScreenWin()
    {
        CanPause(false);
        FindObjectOfType<PauseMenu>().StopTime(true);
        AudioManager.Instance.StopMusicTrack();
        AudioManager.Instance.PlaySoundEffect("LevelWon");
        WinScreenUI.SetActive(true);
    }

    public void StartGameOverScreen()
    {
        gameOver = true;
        CanPause(false);
        AudioManager.Instance.StopMusicTrack();
        AudioManager.Instance.PlaySoundEffect("GameOver");
        gameOverUI.SetActive(true);
    }

    private void CanPause(bool pause)
    {
        canPauseGame = pause;
        FindObjectOfType<PauseMenu>().AllowPause(pause);
    }

    public void SetRespawnPosition(Transform newRespawn)
    {
        
        respawnPlayerPos = newRespawn;
        PlayerPrefs.SetFloat("checkpointPositionX", respawnPlayerPos.position.x);
        PlayerPrefs.SetFloat("checkpointPositionY", respawnPlayerPos.position.y);
    }

    public void StartPlayer()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").transform;

        if(playerCharacter == null)
        {
            RespawnPlayer();
        }
    }

    public void SetPlayer(Transform playerToSet)
    {
        playerCharacter = playerToSet;
    }
}
