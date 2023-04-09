using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutomatedTile : MonoBehaviour
{
    public Tilemap collisionMap;
    public Tile rock;
    public Tilemap tunnelMap;
    public Tile air;

    public int width = 50;
    public int height = 30;
    Vector2 startPoint;


    // Start is called before the first frame update
    void Start()
    {
        startPoint.x = 50;
        startPoint.y = 0;

        SetFull(startPoint);

        Vector2 left = new Vector2(52, 15);
        Vector2 right = new Vector2(98, 15);
        Vector2 up = new Vector2(75, 28);
        Vector2 down = new Vector2(75, 2);

        MakeTunnel(left, up);
        MakeTunnel(right, up);
        MakeTunnel(left, down);
        MakeTunnel(right, down);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetFull(Vector2 startPoint)
    {
        for (int x = (int)startPoint.x; x < (width+(int)startPoint.x); x++)
        {
            for (int y = (int)startPoint.y; y < (height + (int)startPoint.y); y++)
            {
                Vector3Int tempVec = new Vector3Int(x, y, 0);
                collisionMap.SetTile(tempVec, rock);
            }
        }
    }

    private void MakeTunnel(Vector2 startTunnel, Vector2 endTunnel)
    {
        if (startTunnel.x >endTunnel.x)
        {
            if (startTunnel.y > endTunnel.y)
            {
                Vector2 tempVal = startTunnel;
                startTunnel = endTunnel;
                endTunnel = tempVal;
            }
            else
            {
                Vector2 tempVal = startTunnel;
                startTunnel = endTunnel;
                endTunnel = tempVal;
            }
        }

        float pendant = Pendant(startTunnel, endTunnel);
        Debug.Log("Pend: " + pendant);
        float lineStartY = startTunnel.y;

        for (int i = 1; i < 5; i++)
        {
            for (int x = (int)startTunnel.x; x < ((int)endTunnel.x); x++)
            {
                lineStartY += pendant;
                Debug.Log("X: " + x + " Y: " + (int)lineStartY);
                Vector3Int tempVec = new Vector3Int(x, (int)lineStartY, 0);
                collisionMap.SetTile(tempVec, null);
                tunnelMap.SetTile(tempVec, air);
            }
            lineStartY = startTunnel.y + i;
        }
    }

    public float Pendant(Vector2 startLine, Vector2 endLine){

        float pendant = ((startLine.y-endLine.y)/(startLine.x - endLine.x));
        return  pendant;
    }
}
