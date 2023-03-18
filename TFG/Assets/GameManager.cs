using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int vidas;
    private int actualLevel;
    private int points;

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
        vidas = 4;
        actualLevel = 0;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int getVidas()
    {
        return vidas;
    }

    void incVida()
    {
        vidas += 1;
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
        vidas = 4;
        actualLevel = 0;
        points = 0;
    }
}
