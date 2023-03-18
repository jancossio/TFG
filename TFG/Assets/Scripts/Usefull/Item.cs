using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType{
        None, PickUp, Examine
    }
    public InteractionType type;

    public string descriptionItem;
    public Sprite Image;

    public UnityEvent customEvent;

    // Update is called once per frame
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 9;
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractionType.PickUp:
                //Add object to picked list
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                //Delete object
                gameObject.SetActive(false);
                break;

            case InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().Analyzes(this);
                break;

            default:
                Debug.Log("Null Item");
                break;
        }

        //Invoke call to custom event
        customEvent.Invoke();
    }
}
