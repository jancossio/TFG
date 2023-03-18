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
            if(actualItem == 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                FindObjectOfType<ItemManager>().itemObtained(0);

                Destroy(gameObject, 0.5f);
            }
            else if (actualItem == 1)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                FindObjectOfType<ItemManager>().itemObtained(1);

                Destroy(gameObject, 0.5f);
            }
        }
    }
}
