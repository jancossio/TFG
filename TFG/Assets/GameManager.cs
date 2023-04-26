using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private static int lifes;
    private int actualLevel;
    private float health;
    private int points;

    public Transform playerCharacter;
    public Transform respawnPlayerPos;

    
    private bool gameOver = false;
    private bool canPauseGame = true;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject WinScreenUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Más de un GameManager en escena");
        }
    }
    void Start()
    {
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

    void incVida()
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

    void reset()
    {
        lifes = 4;
        PlayerPrefs.SetInt("lifes", lifes);
        //actualLevel = 0;
        //PlayerPrefs.SetInt("actualLevel", actualLevel);
        PlayerPrefs.SetFloat("healthbar", 100);
        points = 0;
    }


    public void RespawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacter, respawnPlayerPos.position, respawnPlayerPos.rotation);
        Debug.Log("Player Respawned in: " + respawnPlayerPos.position);
    }

    public void KillPlayer(PlayerMovement player)
    {
        if (lifes <= 1)
        {
            Destroy(player.gameObject);
        }
        else
        {
            lifes--;
            FreezePlayer(false);
            Instance.RespawnPlayer();
            Destroy(player.gameObject);
        }
    }


    private void FreezePlayer(bool freeze)
    {
        playerCharacter.GetComponent<PlayerMovement>().FreezeRB(freeze);

        GameObject player = playerCharacter.gameObject;
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().FreezePlayer(freeze);
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

    public void StartGameOver()
    {
        Debug.Log("Is Game Over Man!!");
        CanPause(false);
        gameOver = true;
        gameOverUI.SetActive(true);
    }

    public void StartScreenWin()
    {
        Debug.Log("You win Man!!");
        CanPause(false);
        FreezePlayer(true);
        WinScreenUI.SetActive(true);
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
    private void FreezeEnemies()
    {

    }
}
