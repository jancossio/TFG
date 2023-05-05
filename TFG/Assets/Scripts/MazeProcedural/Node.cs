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
    public Tilemap ladderMap;
    public Tile ladderTile;
    public GameObject spikes;
    public GameObject threePlatform;
    public GameObject sixPlatform;
    public GameObject ninePlatform;
    public GameObject diamond;
    public GameObject coin;
    public GameObject turret;
    public GameObject miniEnemy;
    public GameObject mediumEnemy;

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
        //collisionMap = FindObjectOfType<Grid>().transform.FindChild("Foreground").GetComponent<Tilemap>();
        //tunnelMap = FindObjectOfType<Grid>().transform.FindChild("Background").GetComponent<Tilemap>();

        currentState = nodeState.Unvisited;
        currentType = nodeType.None;

        neighbourNodes = new Node[4];
    }


    public List<Node> GetAdjacentNeighbours()
    {
        List<Node> retAdjacent = new List<Node>();
        Node tempNode = null;
        int neighbourIndex = 0;

        while(neighbourIndex < neighbourNodes.Length)
        {
            tempNode = neighbourNodes[neighbourIndex];
            if(tempNode != null)
            {
                if (tempNode.GetState() == nodeState.Unvisited)
                {
                    retAdjacent.Add(tempNode);

                    /*Vector2 neighbourPos = transform.position - tempNode.transform.position;

                    if (neighbourPos.x > 0)
                    {
                        //retAdjacent.Add(1);
                    }
                    else if (neighbourPos.x < 0)
                    {
                        //retAdjacent.Add(3);
                    }
                    else if (neighbourPos.y < 0)
                    {
                        //retAdjacent.Add(2);
                    }
                    else if (neighbourPos.y > 0)
                    {
                        //retAdjacent.Add(4);
                    }*/
                }
            }
            neighbourIndex++;
        }

        return retAdjacent;
    }

    public int GetdirectionByNeighbour(Node neighbour)
    {
        int cardinalRet = 0;
        //bool done = false;

            if (neighbour != null)
            {
                    Vector2 neighbourPos = transform.position - neighbour.transform.position;

                    if (neighbourPos.x > 0 && neighbourPos.y == 0)
                    {
                        cardinalRet = 1;
                        //retNode = searchNode;
                        //done = true;
                    }
                    else if (neighbourPos.x < 0 && neighbourPos.y == 0)
                    {
                        cardinalRet = 3;
                        //retNode = searchNode;
                        //done = true;
                    }
                    else if (neighbourPos.y < 0 && neighbourPos.x == 0)
                    {
                        cardinalRet = 2;
                        //retNode = searchNode;
                        //done = true;
                    }
                    else if (neighbourPos.y > 0 && neighbourPos.x == 0)
                    {
                        cardinalRet = 4;
                        //retNode = searchNode;
                        //done = true;
                    }
            }
        return cardinalRet;
    }

    public void DropWall(int cardinal)
    {
        Debug.Log("Cardinal b like: "+cardinal);
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
        string[] roomMap = new string[10];
        roomMap = FindObjectOfType<MazeGenerator>().GetRoomMap(currentType);
        string temp;
        int y = 0;
        Debug.Log("Now i have room map: "+roomMap);

        for (int i = 0; i < 10; i++)
        {
            temp = roomMap[i];
            int x = 0;
            char valAir = 'a';
            char valRock = 'r';
            char valLadder = 'l';
            char valThreePlatform = 't';
            char valSixPlatform = 'k';
            char valNinePlatform = 'n';
            //char valLadPlatf = 'b';
            char valCoin = 'c';
            char valDiamond = 'd';
            char valSpikes = 's';
            char valTurret = 'o';
            char valMiniEnemy = 'p';
            char valMediEnemy = 'm';

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
                    tunnelMap.SetTile(tempVec, air);
                }
                else if (c.CompareTo(valLadder) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    ladderMap.SetTile(tempVec, ladderTile);
                }
                else if (c.CompareTo(valThreePlatform) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x +x+0.5f, transform.localPosition.y +y+0.5f, 0);
                    Instantiate(threePlatform, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valSixPlatform) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x + 0.5f, transform.localPosition.y + y + 0.5f, 0);
                    Instantiate(sixPlatform, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valNinePlatform) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x + 0.5f, transform.localPosition.y + y + 0.5f, 0);
                    Instantiate(ninePlatform, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valCoin) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x+0.5f, transform.localPosition.y + y+0.5f, 0);
                    Instantiate(coin, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valDiamond) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x+ x + 0.5f, transform.localPosition.y + y + 0.5f, 0);
                    Instantiate(diamond, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valSpikes) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x + 0.5f, transform.localPosition.y + y + 0.3f, 0);
                    Instantiate(spikes, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valTurret) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x + 0.5f, transform.localPosition.y + y + 0.6f, 0);
                    Instantiate(turret, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valMiniEnemy) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x + 0.5f, transform.localPosition.y + y + 0.9f, 0);
                    Instantiate(miniEnemy, objVec, Quaternion.identity);
                }
                else if (c.CompareTo(valMediEnemy) == 0)
                {
                    Vector3Int tempVec = new Vector3Int((int)transform.localPosition.x + x, (int)transform.localPosition.y + y, 0);
                    tunnelMap.SetTile(tempVec, air);
                    Vector3 objVec = new Vector3(transform.localPosition.x + x + 0.5f, transform.localPosition.y + y + 1.1f, 0);
                    Instantiate(mediumEnemy, objVec, Quaternion.identity);
                }
                x++;
            }
            y--;
        }
    }

    public void SetNeighbours(Node[] nodesVar)
    {
        Node tempNode = null;
        Node[] nodeVector = new Node[nodesVar.Length];
        nodeVector = nodesVar;
        int neighbourIndex = 0;
        int nNeighbours = 0;
        bool done = false;

        while (neighbourIndex < nodesVar.Length)
        {
            tempNode = nodeVector[neighbourIndex];
            if (nNeighbours < 4)
            {
                Vector2 neighbourPos = transform.position - tempNode.transform.position;

                //Debug.Log("Conditions B like: " + neighbourPos.x + "TempNode Y: " + neighbourPos.y);
                //Debug.Log("Conditions B like: " + (neighbourPos.x == 16 && neighbourPos.y == 0) + " TempNode Y: " + (neighbourPos.x == -16 && neighbourPos.y == 0) + "TempNode Y: "+ (neighbourPos.x == 0 && neighbourPos.y == 16) + "TempNode Y: " + (neighbourPos.x == 0 && neighbourPos.y == -16));

                if (neighbourPos.x == 16 && neighbourPos.y == 0)
                {
                    neighbourNodes[nNeighbours] = tempNode;
                    nNeighbours++;
                }
                else if (neighbourPos.x == -16 && neighbourPos.y == 0)
                {
                    neighbourNodes[nNeighbours] = tempNode;
                    nNeighbours++;
                }
                else if (neighbourPos.x == 0 && neighbourPos.y == 10)
                {
                    neighbourNodes[nNeighbours] = tempNode;
                    nNeighbours++;
                }
                else if (neighbourPos.x == 0 && neighbourPos.y == -10)
                {
                    neighbourNodes[nNeighbours] = tempNode;
                    nNeighbours++;
                }
            }
            Debug.Log("State nNeighbours: " + nNeighbours + "State neighbourIndex: " + neighbourIndex);
            neighbourIndex++;
        }
    }

    public void SetTilemaps(Tilemap Rock, Tilemap Air, Tilemap ladder)
    {
        tunnelMap = Air;
        collisionMap = Rock;
        ladderMap = ladder;
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
