using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image fillBar;
    float health;
    //public Animator anim;

    private void Start()
    {
        health = 100f;
        PlayerPrefs.SetFloat("healthbar", 100);
        fillBar.fillAmount = health / 100;
    }

    public void LoseHealth(float damage)
    {
        //health = PlayerPrefs.GetFloat("healthbar");
        //Debug.Log("A health b4: " + health);
        health -= damage;
        //Debug.Log("A health after: "+health);
        PlayerPrefs.SetFloat("healthbar", health);

        if(health <= 0)
        {
            FindObjectOfType<LifeCount>().LoseLife();
            FindObjectOfType<PlayerMovement>().Kill();
        }
        fillBar.fillAmount = health / 100;
    }

    public void RefillBar()
    {
        health = 100;
        PlayerPrefs.SetFloat("healthbar", health);
        fillBar.fillAmount = health / 100;
    }

    public void Heal(float healthRecover)
    {
        health += healthRecover;
        health = Mathf.Clamp(health, 0f, 100f);
        fillBar.fillAmount = health / 100;
    }

    public bool checkWounded()
    {
        return (health < 100);
    }

        private void Update()
    {

    }
}
