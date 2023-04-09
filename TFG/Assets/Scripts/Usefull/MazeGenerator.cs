using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        Node[] nodes = new Node[mazeNodeHeight* mazeNodeWidth];

        #region Rooms


        cross = new string[] { "rrraaaaaaaaaarrr",
                               "rraaaaaaaaaaaarr",
                               "raaaaaaaaaaaaarr",
                               "raaarrrrrrraaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "rrrrrraaaarrrrrr",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaar",
                               "aaaarrrrrrrraaar",
                               "aaaaaaaaaaaaaaar",
                               "raaaaaaaaaaarrrr",
                               "rraaaaaaaarrrrrr",
                               "rrrrraaaarrrrrrr"};

        rightT = new string[] {"rraaaaaaaaaaaarr",
                               "rraaaaaaaaaaaarr",
                               "raaaaaaaaaaaaarr",
                               "raaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaaar",
                               "aaaaaaaaaaaaaaar",
                               "aaaaaaaaaaaaaaar",
                               "aaaaaaaaaaaaaarr",
                               "rraaaaaaaaaaarrr",
                               "rrrrraaaaarrrrrr"};

        leftT = new string[]  {"rraaaaaaaaaaaarr",
                               "rraaaaaaaaaaaarr",
                               "rraaaaaaaaaaaarr",
                               "rraarrrrrrraaarr",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaar",
                               "rraaaaaaaaaaaaar",
                               "rraaaaaaaaaaaaar",
                               "rraaaaaaaaaaaarr",
                               "rraaaaaaaaaaarrr",
                               "rrrrraaaarrrrrrr"};

        upT = new string[]   { "rrrrrrrrrrrrrrrr",
                               "rrrrraaaaaaarrrr",
                               "raaaaaaaaaaaaarr",
                               "raaarrrrrrraaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "rrrrrraaaarrrrrr",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaarrrrrrrraaaa",
                               "aaaaaaaaaaaaaaaa",
                               "raaaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaarr",
                               "rrrrraaaaaaaarrr"};

        downT = new string[] { "rrraaaaaaaaarrrr",
                               "rraaaaaaaaaaaarr",
                               "raaaaaaaaaaaaaar",
                               "raaarrrrrrraaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "rrrrrraaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaaaaaaaaaaaaaa",
                               "aaaarrrrrrrraaaa",
                               "aaaaaaaaaaaaaaaa",
                               "raaaaaaaaaaaaarr",
                               "rraaaaaaarrrrrrr",
                               "rrrrrrrrrrrrrrrr"};

        rightUpL = new string[]{"rrraaaaaaaaaarrr",
                               "rraaaaaaaaaaaarr",
                               "raaaaaaaaaaaaarr",
                               "raaarrrrrrraaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "rrrrrraaaarrrrrr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaaaaaaaaaaaarr",
                               "aaaarrrrrrrraarr",
                               "aaaaaaaaaaaaaarr",
                               "raaaaaaaaaaarrrr",
                               "rrrrrrrrrrrrrrrr",
                               "rrrrrrrrrrrrrrrr"};

        leftUpL = new string[]{"rrraaaaaaaaaarrr",
                               "rraaaaaaaaaaaarr",
                               "rraaaaaaaaaaaaaa",
                               "rraarrrrrrraaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraarrrrrrrraaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraarrrrrrrraaaa",
                               "rraaaaaaaaaaaaaa",
                               "rraaaaaaaaaarrrr",
                               "rrrrrrrrrrrrrrrr",
                               "rrrrrrrrrrrrrrrr"};

        rightDownL = new string[]{"rrrrrrrrrrrrrrrr",
                                 "rrrrrrrrrrrrrrrr",
                                 "raaaaaaaaaaaaarr",
                                 "raaarrrrrrraaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "rrrrrraaaarrrrrr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaarrrrrrrraarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "rraaaaaaaaaarrrr",
                                 "rrrraaaaaaaarrrr"};

        leftDownL = new string[]{"rrrrrrrrrrrrrrrr",
                                 "rrrrrrrrrrrrrrrr",
                                 "raaaaaaaaaaaaarr",
                                 "rraarrrrrrraaaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "rrrrrraaaarrrrrr",
                                 "rraaaaaaaaaaaaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "raarrrrrrrrraaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "rraaaaaaaaaaaaaa",
                                 "rraaaaaaaarrrrrr",
                                 "rrrrrraaaaaarrrr"};

        horzCorridor = new string[]{"rrrrrrrrrrrrrrrr",
                                    "rrrrrrrrrrrrrrrr",
                                    "raaaaaaaaaaaaarr",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "aaaaaaaaaaaaaaaa",
                                    "rrrrrrrrrrrrrrrr",
                                    "rrrrrrrrrrrrrrrr"};

        vertCorridor = new string[]{"rraaaaaaaaaaaarr",
                                    "rraaaaaaaaaaaarr",
                                    "raaaaaaaaaaaaarr",
                                    "raaarrrrrrraaarr",
                                    "rraaaaaaaaaaaarr",
                                    "rraaaaaaaaaaaarr",
                                    "rraaaaaaaaaaaarr",
                                    "rrrrrraaaarrrrrr",
                                    "rraaaaaaaaaaaarr",
                                    "rraaaaaaaaaaaarr",
                                    "rraaaaaaaaaaaarr",
                                    "rraarrrrrrraaarr",
                                    "rraaaaaaaaaaaarr",
                                    "rraaaaaaaaaarrrr",
                                    "rraaaaaaaarrrrrr",
                                    "rrrrrraaaarrrrrr"};

        caveRight = new string[]{"rrrrrrrrrrrrrrrr",
                                 "rrrrrrrrrrrrrrrr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaarrrrrrraaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aarrrraaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaarrrrrraaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "aaaaaaaaaaaaaarr",
                                 "rrrrrrrrrrrrrrrr",
                                 "rrrrrrrrrrrrrrrr"};

        caveLeft = new string[]{"rrrrrrrrrrrrrrrr",
                                "rraaaaaaaaaaarrr",
                                "raaaaaaaaaaaaaaa",
                                "raaarrrrrrraaaaa",
                                "rraaaaaaaaaaaaaa",
                                "rraaaaaaaaaaaaaa",
                                "rraaaaaaaaaaaaaa",
                                "rraaaarrrraaaarr",
                                "rraaaaaaaaaaaaaa",
                                "rraaaaaaaaaaaaaa",
                                "rraaaaaaaaaaaaaa",
                                "rraarrrrrrrraaaa",
                                "rraaaaaaaaaaaaaa",
                                "rraaaaaaaaaaaaaa",
                                "rrrrrrrrrrrrrrrr",
                                "rrrrrrrrrrrrrrrr"};

        caveUp = new string[]{"rrrrrrrrrrrrrrrr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaaarr",
                              "rraarrrrrrraaarr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaaarr",
                              "rrrrrraaaarrrrrr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaaarr",
                              "rraarrrrrrraaarr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaaarr",
                              "rraaaaaaaaaaarrr",
                              "rrraaaaaaaaaarrr"};

        caveDown = new string[]{"rraaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rraarrrrrrraaarr",
                                "rraaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rrrrrraaaarrrrrr",
                                "rraaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rraaaaaaaaaaaarr",
                                "rraaaarrrrrraarr",
                                "rraaaaaaaaaaaarr",
                                "rraaaaaaaaaarrrr",
                                "rraaaaaaaarrrrrr",
                                "rrrrrrrrrrrrrrrr"};
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
           BeginProcess();
        }
    }

    void BeginProcess()
    {
        CreateMazeMap();
        BuildMaze();
    }

    void CreateMazeMap()
    {
        currentNode = GetStartNode();
        //visitedNodes.Push(currentNode);
        currentNode.SetState(Node.nodeState.Current);;

        List<int> availablesNextNodes = new List<int>();

        while (visitedNodes.Count < nodes.Length+1)
        {
            mazePath.Push(currentNode);
            availablesNextNodes = currentNode.GetAdjacentNeighbours();

            if (availablesNextNodes.Count > 0)
            {
                int cardinal = availablesNextNodes[Random.Range(0, availablesNextNodes.Count - 1)];
                Debug.Log("NextCardinal: " + cardinal);
                nextNode = currentNode.GetNeighbourByDirection(cardinal);
                Debug.Log("NextNodeCard: " + nextNode);
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
                currentNode = nextNode;
            }
            else
            {
                currentNode.SetState(Node.nodeState.Done);
                visitedNodes.Push(currentNode);

                mazePath.Pop();
                currentNode = mazePath.Peek();

                /*if (mazePath.Count > 1)
                    currentNode = mazePath.Peek();
                //visitedNodes.Push(currentNode);*/
            }

        }
    }

    Node GetStartNode()
    {
        int randomNode = Random.Range(0, nodes.Length);
        Debug.Log("Randooooom: "+ randomNode);
        return nodes[randomNode];
    }

    void BuildMaze()
    {
        Node currentBuildNode;
        for(int i=0; i<nodes.Length; i++)
        {
           currentBuildNode = nodes[i];
            currentBuildNode.BuildRoom();
            Debug.Log("Node: "+i+" Type: "+currentBuildNode.ReturnType());
        }
    }

    public string[] GetRoomMap(Node.nodeType roomType)
    {
        string[] retMap = new string[16];

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

    void EraseMaze()
    {

    }


}
