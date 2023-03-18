using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    //Detection Point
    public Transform detectionPoint;
    //Detection Radius
    private const float detectionRadius = 0.2f;
    //DetectionLayer
    public LayerMask detectionMask;
    //Obtained Object By Trigger
    public GameObject detectedObject;
    //Picked Item list
    public List<GameObject> pickedItems = new List<GameObject>();

    //LookAt window object
    public GameObject lookAtWindow;
    public Image lookAtImage;
    public Text DescriptiveText;

    public bool isAnalyzing;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectDetected())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool ObjectDetected()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionMask);
        if (obj != null){
            detectedObject = obj.gameObject;
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    public void PickUpItem(GameObject newItem)
    {
        pickedItems.Add(newItem);
    }

    public void Analyzes(Item newItem)
    {
        if (isAnalyzing)
        {
            lookAtWindow.SetActive(false);
            isAnalyzing = false;
        }
        else
        {
            lookAtImage.sprite = newItem.GetComponent<SpriteRenderer>().sprite;
            DescriptiveText.text = newItem.descriptionItem;
            lookAtWindow.SetActive(true);
            isAnalyzing = true;
        }
    }
}
