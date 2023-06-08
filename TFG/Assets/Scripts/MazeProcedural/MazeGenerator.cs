using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    public Tilemap collisionMap;
    public Tilemap tunnelMap;
    public Tilemap laddersMap;

    public Transform Player;

    public int mazeNodeHeight;
    public int mazeNodeWidth;
    private float roomHeight = 10;
    private float roomWidth = 16;
    public Node[] mazeNodes;
    Node currentNode = null;
    Node nextNode = null;


    private string[] cross, rightT, leftT, upT, downT, rightUpL, leftUpL, rightDownL, leftDownL, horzCorridor, vertCorridor, caveRight, caveLeft, caveUp, caveDown;

    Stack<Node> mazePath = new Stack<Node>();
    Stack<Node> visitedNodes = new Stack<Node>();

    public Node startNode = null;
    public Node exitNode = null;

    public bool chosenStartNode = false;
    public bool preMadeMaze = false;
    //public bool buildExitStart = false;

    [SerializeField] GameObject InstNode;
    public GameObject ExitDoor;
    public GameObject StartPoint;
    //public Text DoorText;
    public string NextLevel;

    Vector3 minCamBounds = new Vector3(0, 0, 0);
    Vector3 maxCamBounds = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (!preMadeMaze)
        {
            mazeNodes = new Node[mazeNodeHeight * mazeNodeWidth];

            CreateNodes();
            SetCameraBoundaries();
        }

        #region Rooms

        //blueprints for every chamber
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
                               "aaaaacaaalpcaaar",
                               "aaaarrrrrrrraaar",
                               "aaaaaaaaaaaaaaar",
                               "aaaaaaaaaaaaaaar",
                               "rcacracaacaraarr",
                               "rrrrraakaaarrrrr"};

        leftT = new string[]  {"rrrraaaaaaaarrrr",
                               "raaaaaaaaaaaaaar",
                               "raaaaaaaaaaaaaar",
                               "raalacaaacalaaaa",
                               "raalrrrrrrrlaaaa",
                               "ratlaaaaaaalaaaa",
                               "raalaaaaaaalaaar",
                               "raalaaaaaaalaarr",
                               "rcalaracarmlcrrr",
                               "rrrrrratarrrrrrr"};

        upT = new string[]   { "rrrrrrrrrrrrrrrr",
                               "rraaaaaaaaaarrrr",
                               "raaaacaalaacaarr",
                               "raaaaaaklaaaaaaa",
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
                               "aaaalacaacapaaaa",
                               "aaaaaaaanaaalaaa",
                               "aaaaaaaaaaaalaaa",
                               "raaacaacaacalaar",
                               "rrrrrrrrrrrrrrrr"};

        rightUpL = new string[]{"rrraaaaaaaaaarrr",
                               "rraaaaaaaaaaaaar",
                               "raaaaaaaaacaacar",
                               "raaaaaaaarrrrrrr",
                               "aaaaaaaaaaaaaaar",
                               "aaaacaaacaaaaaar",
                               "aaarrrrrrraaaaar",
                               "aaaaaaaaaaaaaaar",
                               "raaaaaaamaaaassr",
                               "rrrrrrrrrrrrrrrr"};

        leftUpL = new string[]{"rrraaaaaaaaaarrr",
                               "rralacaaacaaaarr",
                               "raalrrrrrrraaaaa",
                               "raalaaaaaaaaaaaa",
                               "raalaaaaaaamcaaa",
                               "raalaaaaaaaataaa",
                               "rlalaaaaaaaaaaaa",
                               "rlrrrraaaaaaarra",
                               "rlaaadraamaarrrr",
                               "rrrrrrrrrrrrrrrr"};

        rightDownL = new string[]{"rrrrrrrrrrrrrrrr",
                                  "raaaaaraaaaaaaar",
                                  "raaaaaradacalaar",
                                  "raaaaarrrrrrlaar",
                                  "aaaaaaaaaaaalaar",
                                  "aaaaaaaaaaaalaar",
                                  "aaaaaaaaaaaalaar",
                                  "aaaaacaacaoalaar",
                                  "raaraaaaaararaar",
                                  "rrrraakaaarrrrrr"};

        leftDownL = new string[]{"rrrrrrrrrrrrrrrr",
                                 "raaaaaaaaaaaaarr",
                                 "raaaaaaaaaaaaarr",
                                 "raalacaoacaaaaaa",
                                 "raalrrrrrrraaaaa",
                                 "raalaaaaaaaaaaaa",
                                 "raalaaaaaaaaaaaa",
                                 "rralaaaaaaacaaca",
                                 "rrrlaaaaaarrrrrr",
                                 "rrrrrraaaaaarrrr"};

        horzCorridor = new string[]{"rrrrrrrrrrrrrrrr",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aalaaacpcaaalaaa",
                                    "atlaarrrrraaltaa",
                                    "aalaaaaaaaaalaaa",
                                    "aalaaacaacaalaaa",
                                    "aalaaaaaaaaalaaa",
                                    "ralaasapasaalaar",
                                    "rrrrrrrrrrrrrrrr"};

        vertCorridor = new string[]{"raaaaaaaaaaaaaar",
                                    "raaaaalcaaacaaar",
                                    "raaaaalakaaaaaar",
                                    "raaaaalaaaaaaaar",
                                    "raaaoalaaaalaaar",
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
                                 "rrraaaaamaaaaarr",
                                 "rrrrrrrrrrrrrrrr"};

        caveLeft = new string[]{"rrrrrrrrrrrrrrrr",
                                "raaaaaaaaaaaaaar",
                                "rldraaaaaaaaaaar",
                                "rlrraaaaaaaaaaar",
                                "rlraacaaacaaaaar",
                                "rlrarrrrrrraaaar",
                                "rlraaaaaaaaaaaar",
                                "rlrrrrraaaaaaaaa",
                                "rlaacaacaacaarrr",
                                "rrrrrrrrrrrrrrrr"};

        caveUp = new string[]{"rrrrrrrrrrrrrrrr",
                              "raaaaaaaaaaaarrr",
                              "raaaaaaaaaaaarar",
                              "raaaaaaaaaaaaaar",
                              "raaaacaaacaaaaar",
                              "raaaarrrrrrraaar",
                              "raaaaaaaaaaaaaar",
                              "rraaaaaaaaaaaaar",
                              "raaaaaaaaaamaaar",
                              "rrrraaaaaarrrrrr"};

        caveDown = new string[]{"raaaaaaaaaaaaaar",
                                "raaaaaaaaaaaaaar",
                                "raaaaaaaaakaaarr",
                                "raaaaaaaaaaaaaar",
                                "raaaacaaacaaaaar",
                                "raaarrrrrrraaaar",
                                "raaaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rrrraaaamaaaarrr",
                                "rrrrrrrrrrrrrrrr"};
        #endregion

        StartNodes();
        CreateMazeMap();
        BuildMaze();

        if (!preMadeMaze)
        {
            SetStartExit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.N))
        {
            CreateMazeMap();
            BuildMaze();
            SetStartExit();
        }*/
    }

    void CreateMazeMap()
    {
        int longestWay = 0;
        currentNode = GetStartNode();

        currentNode.SetState(Node.nodeState.Current);

        List<Node> availablesNextNodes = new List<Node>();

        while (visitedNodes.Count < mazeNodes.Length)
        {
            availablesNextNodes = currentNode.GetAdjacentNeighbours();

            if (availablesNextNodes.Count > 0)
            {
                nextNode = availablesNextNodes[Random.Range(0, availablesNextNodes.Count - 1)];
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

                /*if (!visitedNodes.Contains(currentNode))
                {
                    visitedNodes.Push(currentNode);
                }*/

                currentNode = nextNode;
                Debug.Log("The node length is: " + mazeNodes.Length);
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
                        //Debug.Log("The node is done: " + currentNode);
                        currentNode = mazePath.Peek();
                    }
                }

                Debug.Log("Total nodes visited: " + visitedNodes.Count);
            }

        }
        //Debug.Log("Now im free: ");
    }

    Node GetStartNode()
    {
        int randomNode = Random.Range(0, mazeNodes.Length);
        startNode = mazeNodes[randomNode];
        return startNode;
    }

    void BuildMaze()
    {
        Debug.Log("Total nodes visited posterior: " + visitedNodes.Count);

        Node currentBuildNode;
        for(int i=0; i< mazeNodes.Length; i++)
        {
           currentBuildNode = mazeNodes[i];
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
                mazeNodes[temp] = obj.GetComponent<Node>();
                mazeNodes[temp].SetTilemaps(collisionMap, tunnelMap, laddersMap);
                temp++;
            }
        }
    }

    void StartNodes()
    {
        Node tempNode = null;

        for(int i = 0; i< mazeNodes.Length; i++)
        {
            tempNode = mazeNodes[i];
            tempNode.SetNeighbours(mazeNodes);
        }
    }

    void SetStartExit()
    {
        //End
        Vector3 exitTemp = new Vector3((exitNode.transform.position.x + 8.5f), (exitNode.transform.position.y - 3f), 0);
        GameObject exit = Instantiate(ExitDoor, exitTemp, Quaternion.identity) as GameObject;
        //exit.GetComponent<EndingDoor>().SetParameters(DoorText, NextLevel);

        //Beggining
        Vector3 startTemp = new Vector3((startNode.transform.position.x + 8.5f), (startNode.transform.position.y), 0);
        Debug.Log("StartPos is: "+startTemp);
        GameObject startPos = Instantiate(StartPoint, startTemp, Quaternion.identity) as GameObject;
        GameManager.Instance.SetRespawnPosition(startPos.transform);
        Debug.Log("Checkpoint is: "+startPos.transform.position);
        startPos.GetComponent<SpriteRenderer>().enabled = false;
        startPos.GetComponent<Checkpoint>().SetBounds(minCamBounds, maxCamBounds);
        startPos.GetComponent<Checkpoint>().sounds = false;
        GameManager.Instance.SetPlayer(Player);
        GameManager.Instance.RespawnPlayer();
    }

    private void SetCameraBoundaries()
    {
        float minX, minY, maxX, maxY;

        float tempX = (float)mazeNodeWidth;
        float tempY = (float)mazeNodeHeight;

        minX = (-roomWidth/2) + 2;
        minY = roomHeight/2;
        maxX = (tempX - 0.5f) * roomWidth;
        maxY = (tempY - 0.5f) * roomHeight;

        minCamBounds = new Vector3(minX, minY, -10f);
        maxCamBounds = new Vector3(maxX, maxY, -10f);

        Debug.Log("eeee tu camara estupida");

        GameObject.FindObjectOfType<CameraFollow>().SetNewCheckpointBounds(minCamBounds, maxCamBounds);
    }
}
