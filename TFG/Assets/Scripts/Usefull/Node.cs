using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node : MonoBehaviour
{
    public Tilemap collisionMap;
    public Tile rock;
    public Tilemap tunnelMap;
    public Tile air;

    public Node[] neighbourNodes;

    public bool upWall = true;
    public bool downWall = true;
    public bool leftWall = true;
    public bool rightWall = true;

    public enum nodeState
    {
        Unvisited, Current, Done, Searching
    }

    public nodeState currentState;

    public enum nodeType
    {
        Cross, RightT, LeftT, UpT, DownT, RightUpL, LeftUpL, RightDownL, LeftDownL, HorzCorridor, VertCorridor, CaveRight, CaveLeft, CaveUp, CaveDown, None
    }

    nodeType currentType;

    // Start is called before the first frame update
    void Start()
    {
        currentState = nodeState.Unvisited;
        currentType = nodeType.None;
    }


    public List<int> GetAdjacentNeighbours()
    {
        List<int> retAdjacent = new List<int>();
        Node tempNode = null;
        int neighbourIndex = 0;

        while(neighbourIndex < neighbourNodes.Length)
        {
            tempNode = neighbourNodes[neighbourIndex];
            if(tempNode.GetState() == nodeState.Unvisited)
            {
                Vector2 neighbourPos = transform.position - tempNode.transform.position;

                if (neighbourPos.x > 0)
                {
                    retAdjacent.Add(1);
                }
                else if (neighbourPos.x < 0)
                {
                    retAdjacent.Add(3);
                }
                else if (neighbourPos.y < 0)
                {
                    retAdjacent.Add(2);
                }
                else if (neighbourPos.y > 0)
                {
                    retAdjacent.Add(4);
                }
            }
            neighbourIndex++;
        }

        return retAdjacent;
    }

    public Node GetNeighbourByDirection(int cardinal)
    {
        Node retNode = null;
        Node searchNode = null;
        int neighbourIndex = 0;
        bool done = false;

        while (neighbourIndex < neighbourNodes.Length)
        {
            searchNode = neighbourNodes[neighbourIndex];
            if (searchNode.GetState() == nodeState.Unvisited && !done)
            {
                Vector2 neighbourPos = transform.position - searchNode.transform.position;

                if (neighbourPos.x > 0 && cardinal==1)
                {
                    retNode = searchNode;
                    done = true;
                }
                else if (neighbourPos.x < 0 && cardinal == 3)
                {
                    retNode = searchNode;
                    done = true;
                }
                else if (neighbourPos.y < 0 && cardinal == 2)
                {
                    retNode = searchNode;
                    done = true;
                }
                else if (neighbourPos.y > 0 && cardinal == 4)
                {
                    retNode = searchNode;
                    done = true;
                }
            }
            neighbourIndex++;
        }

        return retNode;
    }

    public void DropWall(int cardinal)
    {
        switch (cardinal)
        {
            case 1:
                leftWall = false;
                break;
            case 2:
                upWall = false;
                break;
            case 3:
                rightWall = false;
                break;
            case 4:
                downWall = false;
                break;
        }
        CheckNodeType();
    }

    public void CheckNodeType()
    {
        if(!upWall && !downWall && !leftWall && !rightWall)
        {
            currentType = nodeType.Cross;
        }
        else if(!upWall && !downWall && leftWall && !rightWall)
        {
            currentType = nodeType.LeftT;
        }
        else if(upWall && !downWall && !leftWall && !rightWall)
        {
            currentType = nodeType.UpT;
        }
        else if(!upWall && !downWall && !leftWall && rightWall)
        {
            currentType = nodeType.RightT;
        }
        else if(!upWall && downWall && !leftWall && !rightWall)
        {
            currentType = nodeType.DownT;
        }
        else if(!upWall && downWall && leftWall && !rightWall)
        {
            currentType = nodeType.LeftUpL;
        }
        else if(!upWall && downWall && !leftWall && rightWall)
        {
            currentType = nodeType.RightUpL;
        }
        else if (upWall && !downWall && leftWall && !rightWall)
        {
            currentType = nodeType.LeftDownL;
        }
        else if (upWall && !downWall && !leftWall && rightWall)
        {
            currentType = nodeType.RightDownL;
        }
        else if (!upWall && !downWall && leftWall && rightWall)
        {
            currentType = nodeType.VertCorridor;
        }
        else if (upWall && downWall && !leftWall && !rightWall)
        {
            currentType = nodeType.HorzCorridor;
        }
        else if(upWall && downWall && leftWall && !rightWall)
        {
            currentType = nodeType.CaveLeft;
        }
        else if(upWall && downWall && !leftWall && rightWall)
        {
            currentType = nodeType.CaveRight;
        }
        else if(!upWall && downWall && leftWall && rightWall)
        {
            currentType = nodeType.CaveDown;
        }
        else if(upWall && !downWall && leftWall && rightWall)
        {
            currentType = nodeType.CaveUp;
        }
    }

    public void BuildRoom()
    {
        string[] roomMap = new string[16];
        roomMap = FindObjectOfType<MazeGenerator>().GetRoomMap(currentType);
        string temp;
        int y = 0;

        for (int i = 0; i < 16; i++)
        {
            temp = roomMap[i];
            int x = 0;
            char valAir = 'a';
            char valRock = 'r';
            foreach (char c in temp)
            {
                if (c.CompareTo(valRock) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x+x, (int)transform.localPosition.y+y, 0);
                    collisionMap.SetTile(tempVec, rock);
                }
                else if(c.CompareTo(valAir) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    collisionMap.SetTile(tempVec, air);
                }
                x++;
            }
            y--;
        }
    }

    public string ReturnType()
    {
        return currentType.ToString();
    }

    public void SetState(nodeState state)
    {
        currentState = state;
    }

    public nodeState GetState() {
        return currentState;
    }

}
