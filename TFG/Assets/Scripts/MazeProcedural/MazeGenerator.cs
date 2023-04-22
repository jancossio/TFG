﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MazeGenerator : MonoBehaviour
{
    public Tilemap collisionMap;
    public Tilemap tunnelMap;
    public Tilemap laddersMap;


    public int mazeNodeHeight;
    public int mazeNodeWidth;
    public Node[] nodes;
    Node currentNode = null;
    Node nextNode = null;
    //Node tempNode = null;
    private int currentNodePosX = 0;
    private int currentNodePosY = 0;


    private string[] cross, rightT, leftT, upT, downT, rightUpL, leftUpL, rightDownL, leftDownL, horzCorridor, vertCorridor, caveRight, caveLeft, caveUp, caveDown;

    Stack<Node> mazePath = new Stack<Node>();
    Stack<Node> visitedNodes = new Stack<Node>();

    public Node startNode = null;
    public Node exitNode = null;

    [SerializeField] GameObject InstNode;
    public GameObject ExitDoor;
    public GameObject StartPoint;
    public Text DoorText;
    public string NextLevel;

    // Start is called before the first frame update
    void Start()
    {
        nodes = new Node[mazeNodeHeight*mazeNodeWidth];

        CreateNodes();
        StartNodes();

        #region Rooms


        cross = new string[] { "rrraaaaaaaaaarrr",
                               "rraaaaaaaaaaaarr",
                               "raaaaaaaaaaaaarr",
                               "raaarrrrrrraaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaaar",
                               "raaaaaaaaaaarrrr",
                               "rraaaaaaaarrrrrr",
                               "rrrrraaaarrrrrrr"};

        rightT = new string[] {"rraaaaaaaaaarrrr",
                               "rraacaacalaaaarr",
                               "raarrrrrrlaaaaar",
                               "raaaaaaaalaaaaar",
                               "aaaaacaaalacaaar",
                               "aaaarrrrrrrraaar",
                               "aaaaaaaaaaaaaaar",
                               "aaaaaaaaaaaaaaar",
                               "rcacracaacaraarr",
                               "rrrrraakaaarrrrr"};

        leftT = new string[]  {"rrrraaaaaaaarrrr",
                               "raaaaaaaaaaaaaar",
                               "raalacaaacaaaaar",
                               "ratlrrrrrrraaaar",
                               "raalaaaaaaaaaaaa",
                               "raalaaaaaaaaaaaa",
                               "raalaaaaaaaaaaar",
                               "raalaaaaaaaaaarr",
                               "rcalaracaracarrr",
                               "rrrrrratarrrrrrr"};

        upT = new string[]   { "rrrrrrrrrrrrrrrr",
                               "rrrrraaaaaaarrrr",
                               "raaaaaaalaaaaarr",
                               "raaaaaaakaaaaaaa",
                               "aaaaaalalaaaaaaa",
                               "aaaaaalkaaaaaaaa",
                               "aaaaaalaaaaaaaaa",
                               "aaaaaalaaaaaaaca",
                               "rraraalcaaacarrr",
                               "rrrraaaanaaaarrr"};

        downT = new string[] { "rrrraaaaaaaaarrr",
                               "raaaaaaaaaaaaaar",
                               "raaalacaacaaaaar",
                               "raaalakaaaaaaaaa",
                               "raaalaaaaaaaaaaa",
                               "aaaalacaacaaaaaa",
                               "aaaaaaaanaaalaaa",
                               "aaaaaaaaaaaalaaa",
                               "raaacaacaacalaar",
                               "rrrrrrrrrrrrrrrr"};

        rightUpL = new string[]{"rrraaaaaaaaaarrr",
                               "rraaaaaaaaaaaaar",
                               "raaaaaaaaacaacar",
                               "raaaaaaaarrrrrrr",
                               "aaaaaaaaaaaaaaar",
                               "aaaacacacaaaaaar",
                               "aaarrrrrrraaaaar",
                               "aaaaaaaaaaaaaaar",
                               "raaaaaaaaasssssr",
                               "rrrrrrrrrrrrrrrr"};

        leftUpL = new string[]{"rrraaaaaaaaaarrr",
                               "rralacaaacaaaarr",
                               "raalrrrrrrraaaaa",
                               "raalaaaaaaaaaaaa",
                               "raalaaaaaaaacaaa",
                               "raalaaaaaaaataaa",
                               "rlalaaaaaaaaaaaa",
                               "rlrrrraaaaaaaarr",
                               "rlaaadraaaaaarrr",
                               "rrrrrrrrrrrrrrrr"};

        rightDownL = new string[]{"rrrrrrrrrrrrrrrr",
                                  "raaaaaraaaaaaaar",
                                  "raaaaaradaaalaar",
                                  "raaaaarrrrrrlaar",
                                  "aaaaaaaaaaaalaar",
                                  "aaaaaaaaaaaalaar",
                                  "aaaaaaaaaaaalaar",
                                  "aaaaacaacaaalaar",
                                  "raaraaaaaararaar",
                                  "rrrraakaaarrrrrr"};

        leftDownL = new string[]{"rrrrrrrrrrrrrrrr",
                                 "raaaaaaaaaaaaarr",
                                 "raaaaaaaaaaaaarr",
                                 "raalacaaacaaaaaa",
                                 "raalrrrrrrraaaaa",
                                 "raalaaaaaaaaaaaa",
                                 "raalaaaaaaaaaaaa",
                                 "rralaaaaaaacaaca",
                                 "rrrlaaaaaarrrrrr",
                                 "rrrrrraaaaaarrrr"};

        horzCorridor = new string[]{"rrrrrrrrrrrrrrrr",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "alaaaacacaaaaala",
                                    "altaarrrrraaatla",
                                    "alaaaaaaaaaaaala",
                                    "alaaaacaacaaaala",
                                    "alaaaaaaaaaaaala",
                                    "rlaaasaassaaaalr",
                                    "rrrrrrrrrrrrrrrr"};

        vertCorridor = new string[]{"raaaaaaaaaaaaaar",
                                    "raaaaalcaaacaaar",
                                    "raaaaalakaaaaaar",
                                    "raaaaalaaaaaaaar",
                                    "raaaaalaaaalaaar",
                                    "raaaaaaanaalaaar",
                                    "raaaaaaaaaalaaar",
                                    "raaaaaaaaaalaarr",
                                    "racaraaaaaaraarr",
                                    "rrrrraakaaarrrrr"};

        caveRight = new string[]{"rrrrrrrrrrrrrrrr",
                                 "raaaaaaaaaaaaaar",
                                 "racaaaaaaaaaacar",
                                 "rataaaaaaaaaatar",
                                 "raaaacaaacaaaaar",
                                 "raaarrrrrrraaaar",
                                 "rraaaaaaaaaaaaar",
                                 "aaaaaaaaaaaaaaar",
                                 "rrraaaaaaaaaaarr",
                                 "rrrrrrrrrrrrrrrr"};

        caveLeft = new string[]{"rrrrrrrrrrrrrrrr",
                                "raararaaaaraarar",
                                "rldraaaaaaraaaar",
                                "rlrraaaaaaaaaaar",
                                "rlraacaaacaaaaar",
                                "rlrarrrrrrraaaar",
                                "rlraaaaaaaaaaaar",
                                "rlrrrrraaaaaaaaa",
                                "rlaacaacaacaarrr",
                                "rrrrrrrrrrrrrrrr"};

        caveUp = new string[]{"rrrrrrrrrrrrrrrr",
                              "rrraraaararaarrr",
                              "raaaaaaaaaaaarar",
                              "raaaaaaaaaaaaaar",
                              "raaaacaaacaaaaar",
                              "raaaarrrrrrraaar",
                              "raaaaaaaaaaaaaar",
                              "rraaaaaaaaaaaaar",
                              "raaaaaaaaaaaaaar",
                              "rrrraaaaaarrrrrr"};

        caveDown = new string[]{"raaaaaaaaaaaaaar",
                                "raaaaaaaaaaaaaar",
                                "raaaaaaaaakaaarr",
                                "raaaaaaaaaaaaaar",
                                "raaaacaaacaaaaar",
                                "raaarrrrrrraaaar",
                                "raaaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rrrraaaaaaaaarrr",
                                "rrrrrrrrrrrrrrrr"};
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            CreateMazeMap();
            BuildMaze();
            SetStartExit();
        }
    }

    void CreateMazeMap()
    {
        int longestWay = 0;
        currentNode = GetStartNode();
        currentNode.SetState(Node.nodeState.Current);

        List<Node> availablesNextNodes = new List<Node>();

        while (visitedNodes.Count < nodes.Length)
        {
            availablesNextNodes = currentNode.GetAdjacentNeighbours();

            if (availablesNextNodes.Count > 0)
            {
                nextNode = availablesNextNodes[Random.Range(0, availablesNextNodes.Count - 1)];
                //Debug.Log("NextCardinal: " + cardinal);
                //nextNode = currentNode.GetNeighbourByDirection(cardinal);
                //nextNode = availablesNextNodes[cardinal];
                //Debug.Log("NextNodeCard: " + nextNode);
                int cardinal = currentNode.GetdirectionByNeighbour(nextNode);
                Debug.Log("From: " +currentNode.transform.position+" To: "+nextNode.transform.position);
                switch (cardinal)
                {
                    case 1:
                        currentNode.DropWall(1);
                        nextNode.DropWall(3);
                        break;
                    case 2:
                        currentNode.DropWall(2);
                        nextNode.DropWall(4);
                        break;
                    case 3:
                        currentNode.DropWall(3);
                        nextNode.DropWall(1);
                        break;
                    case 4:
                        currentNode.DropWall(4);
                        nextNode.DropWall(2); ;
                        break;
                }
                currentNode.SetState(Node.nodeState.Searching);
                nextNode.SetState(Node.nodeState.Current);
                mazePath.Push(currentNode);

                if (!visitedNodes.Contains(currentNode))
                {
                    visitedNodes.Push(currentNode);
                }

                currentNode = nextNode;
                Debug.Log("The node length is: " + nodes.Length);
            }
            else
            {
                currentNode.SetState(Node.nodeState.Done);

                if (!visitedNodes.Contains(currentNode))
                 {
                     visitedNodes.Push(currentNode);
                 }

                    if (currentNode != mazePath.Peek())
                    {
                        if(longestWay < mazePath.Count)
                        {
                            exitNode = currentNode;
                            longestWay = mazePath.Count;
                        }
                        currentNode = mazePath.Peek();
                    }
                    else
                    {
                        mazePath.Pop();

                        if (mazePath.Count >= 1)
                        {
                            currentNode = mazePath.Peek();
                        }
                    }
                Debug.Log("The stack num is: " + mazePath.Count);
            }

        }
        Debug.Log("Now im free: ");
    }

    Node GetStartNode()
    {
        int randomNode = Random.Range(0, nodes.Length);
        Debug.Log("Randooooom: "+ randomNode);
        startNode = nodes[randomNode];
        return startNode;
    }

    void BuildMaze()
    {
        Node currentBuildNode;
        for(int i=0; i<nodes.Length; i++)
        {
           currentBuildNode = nodes[i];
           currentBuildNode.BuildRoom();
           //Debug.Log("Node: "+i+" Type: "+currentBuildNode.ReturnType());
        }
    }

    public string[] GetRoomMap(Node.nodeType roomType)
    {
        string[] retMap = new string[10];

        switch (roomType)
        {
            case Node.nodeType.Cross:
                retMap = cross;
                break;
            case Node.nodeType.RightT:
                retMap = rightT;
                break;
            case Node.nodeType.LeftT:
                retMap = leftT;
                break;
            case Node.nodeType.UpT:
                retMap = upT;
                break;
            case Node.nodeType.DownT:
                retMap = downT;
                break;
            case Node.nodeType.RightUpL:
                retMap = rightUpL;
                break;
            case Node.nodeType.LeftUpL:
                retMap = leftUpL;
                break;
            case Node.nodeType.RightDownL:
                retMap = rightDownL;
                break;
            case Node.nodeType.LeftDownL:
                retMap = leftDownL;
                break;
            case Node.nodeType.HorzCorridor:
                retMap = horzCorridor;
                break;
            case Node.nodeType.VertCorridor:
                retMap = vertCorridor;
                break;
            case Node.nodeType.CaveRight:
                retMap = caveRight;
                break;
            case Node.nodeType.CaveLeft:
                retMap = caveLeft;
                break;
            case Node.nodeType.CaveUp:
                retMap = caveUp;
                break;
            case Node.nodeType.CaveDown:
                retMap = caveDown;
                break;
        }

        return retMap;
    }

    void CreateNodes()
    {
        int temp = 0;
        for (int y = 0; y < mazeNodeHeight; y++)
        {
            for (int x = 0; x < mazeNodeWidth; x++)
            {
                Vector3 tempVec = new Vector3(transform.position.x+(x*16), transform.position.y + (y *10), 0);
                GameObject obj = Instantiate(InstNode, tempVec, Quaternion.identity) as GameObject;
                nodes[temp] = obj.GetComponent<Node>();
                nodes[temp].SetTilemaps(collisionMap, tunnelMap, laddersMap);
                temp++;
            }
        }
    }

    void StartNodes()
    {
        Node tempNode = null;

        for(int i = 0; i<nodes.Length; i++)
        {
            tempNode = nodes[i];
            tempNode.SetNeighbours(nodes);
        }
    }

    void SetStartExit()
    {
        //End
        Vector3 exitTemp = new Vector3((exitNode.transform.position.x + 8.5f), (exitNode.transform.position.y - 3f), 0);
        GameObject exit = Instantiate(ExitDoor, exitTemp, Quaternion.identity) as GameObject;
        exit.GetComponent<EndingDoor>().SetParameters(DoorText, NextLevel);

        //Beggining
        Vector3 startTemp = new Vector3((startNode.transform.position.x + 8.5f), (startNode.transform.position.y - 3f), 0);
        Debug.Log("StartPos is: "+startTemp);
        GameObject startPos = Instantiate(StartPoint, startTemp, Quaternion.identity) as GameObject;
        GameManager.Instance.SetRespawnPosition(startPos.transform);
        Debug.Log("Checkpoint is: "+startPos.transform.position);
        startPos.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.Instance.RespawnPlayer();
    }
}