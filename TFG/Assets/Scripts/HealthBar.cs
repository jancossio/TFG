using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image fillBar;
    float health;
    public Animator anim;

    private void Start()
    {
        fillBar.fillAmount = health / 100;
    }

    public void LoseHealth(int damage)
    {
        health = PlayerPrefs.GetFloat("healthbar");
        health -= damage;
        PlayerPrefs.SetFloat("healthbar", health);
        fillBar.fillAmount = health / 100;

        if(health <= 0)
        {
            FindObjectOfType<LifeCount>().LoseLife();
        }
    }

    public void RefillBar()
    {
        health = 100;
        PlayerPrefs.SetFloat("healthbar", health);
        fillBar.fillAmount = health / 100;
    }

        private void Update()
    {

    }
}
