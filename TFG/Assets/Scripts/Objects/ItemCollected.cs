using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour
{
    //"The value 0 is used for cherries, & the value 1 is used for diamonds"
    public int actualItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (actualItem <= 1)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                FindObjectOfType<ItemManager>().itemObtained(actualItem);
                Destroy(gameObject, 0.5f);

            }
            else if(actualItem == 2)
            {
                bool tempCheck = FindObjectOfType<LifeCount>().CheckMissingLifes();
                if (tempCheck)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    FindObjectOfType<ItemManager>().itemObtained(actualItem);
                    Destroy(gameObject, 0.5f);
                }
            }
            else if (actualItem >=3 && actualItem <= 4)
            {
                bool tempCheck = FindObjectOfType<HealthBar>().checkWounded();
                if (tempCheck)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    FindObjectOfType<ItemManager>().itemObtained(actualItem);
                    Destroy(gameObject, 0.5f);
                }
            }
            else if (actualItem >= 5)
            {
                int ammoCheck = FindObjectOfType<ItemManager>().checkNumPines();
                //Debug.Log("Deberia llegar aqui: " + ammoCheck);
                if (ammoCheck < 15)
                {
                    //Debug.Log("Deberia llegar aqui: " + ammoCheck);
                    GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    FindObjectOfType<ItemManager>().itemObtained(actualItem);
                    Destroy(gameObject, 0.5f);
                }
            }
        }
    }
}
