using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this will hold the actual algorithm for A star
public static class AStar
{
    /// <summary>
    /// Builds a path for the room, from the startGridPosition to the endGridPosition, and adds
    /// movement steps to the returned Stack. Returns null if no path is found.
    /// </summary>
    public static Stack<Vector3> BuildPath(Room room, Vector3Int startGridPosition, Vector3Int endGridPosition)
    {
        // Adjust positions by lower bounds
        startGridPosition -= (Vector3Int)room.templateLowerBounds;
        endGridPosition -= (Vector3Int)room.templateLowerBounds;

        // create an open list and closed hashset
        List<Node> openNodeList = new List<Node>();
        // why hashset? 
        // because an optimized collection of unordered, unique elements that provides fast lookups and high-performance set operations. 
        //also no duplicate elements this way
        HashSet<Node> closedNodeHashSet = new HashSet<Node>();

        // create Grid nodes for path finding
        GridNodes gridNodes = new GridNodes(room.templateUpperBounds.x - (room.templateLowerBounds.x + 1), 
                                            room.templateUpperBounds.y - (room.templateLowerBounds.y + 1));

        Node startNode = gridNodes.GetGridNode(startGridPosition.x, startGridPosition.y);
        Node targetNode = gridNodes.GetGridNode(endGridPosition.x, endGridPosition.y);
        Node endPathNode = FindShortestPath(startNode, targetNode, gridNodes, openNodeList, closedNodeHashSet, room.instantiatedRoom);

        if(endPathNode != null)
        {
            return CreatePathStack(endPathNode, room);
        }


        return null;
    }

    /// <summary>
    /// Find the shortest path - returns the end Node if a path has been found, else returns null.
    /// </summary>
    private static Node FindShortestPath(Node startNode, Node targetNode, GridNodes gridNodes, List<Node> openNodeList, HashSet<Node> closedNodeHashSet, InstantiatedRoom instantiatedRoom)
    {
        // Add a start Node to open List
        openNodeList.Add(startNode);

        // next we loop through the open list until we find it empty
        while(openNodeList.Count > 0)
        {
            // Sort the list
            openNodeList.Sort();

            // current Node in the open list will be the one with the lowest final cost (remember the working table)
            Node currentNode = openNodeList[0];
            openNodeList.RemoveAt(0);

            // if the current node is equal to our target node then we want to finish the path and return current node
            if(currentNode == targetNode)
            {
                return currentNode;
            }

            // add this current node to the closed list
            closedNodeHashSet.Add(currentNode);

            // lastly  we calculate the final cost for each neighbour of the current node 
            EvaluateCurrentNodeNeighbours(currentNode, targetNode, gridNodes, openNodeList, closedNodeHashSet, instantiatedRoom);
        }
        return null;
    }
    

    /// <summary>
    ///  Create a Stack<Vector3> containing the movement path 
    /// </summary>
    private static Stack<Vector3> CreatePathStack(Node targetNode, Room room)
    {
        Stack<Vector3> movementPathStack = new Stack<Vector3>();

        Node nextNode = targetNode;

        //get mid point of the cell
        Vector3 cellMidPoint = room.instantiatedRoom.grid.cellSize * 0.5f;
        cellMidPoint.z = 0f;

        while(nextNode != null)
        {
            // Convert grid position to world position
            Vector3 worldPosition = room.instantiatedRoom.grid.CellToWorld(new Vector3Int ( 
                                                        (nextNode.gridPosition.x + room.templateLowerBounds.x), 
                                                        (nextNode.gridPosition.y + room.templateLowerBounds.y), 
                                                        0) );

            // set the world position to the middle of the grid cell
            worldPosition += cellMidPoint;

            movementPathStack.Push(worldPosition);

            nextNode = nextNode.parentNode;

        }

        return movementPathStack;
    }

    /// <summary>
    /// Evaluate neighbour nodes
    /// </summary>
    private static void EvaluateCurrentNodeNeighbours(Node currentNode, Node targetNode, GridNodes gridNodes, List<Node> openNodeList, HashSet<Node> closedNodeHashSet, InstantiatedRoom instantiatedRoom)
    {
        Vector2Int currentNodeGridPosition = currentNode.gridPosition;
        Node validNeighbourNode;

        //loop through in every direction
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                validNeighbourNode = GetValidNodeNeighbour( (currentNodeGridPosition.x + i), (currentNodeGridPosition.y + j),
                                                            gridNodes, closedNodeHashSet, instantiatedRoom);

                if(validNeighbourNode != null)
                {
                    // calculate the new cost to trave from one neighbour node to another (i.e. g(n) cost)
                    int newCostToNeighbour;

                    /// <summary>
                    /// Then get the movement penalty
                    /// basically unwalkable paths have a value of 0.
                    /// Will set a default penalty value in the Settings.cs
                    /// This will then also apply to other grid squares
                    /// </summary>
                    int movemmovementPenaltyForGridSpace = instantiatedRoom.aStarMovementPenalty[validNeighbourNode.gridPosition.x, validNeighbourNode.gridPosition.y];

                    newCostToNeighbour = currentNode.traverseNodeCost + GetDistance(currentNode, validNeighbourNode) + movemmovementPenaltyForGridSpace;

                    bool isValidNeighbourNodeInOpenList = openNodeList.Contains(validNeighbourNode);

                    if (newCostToNeighbour < validNeighbourNode.traverseNodeCost || !isValidNeighbourNodeInOpenList)
                    {
                        validNeighbourNode.traverseNodeCost = newCostToNeighbour;
                        validNeighbourNode.heuristicApproxNodeCost = GetDistance(validNeighbourNode, targetNode);
                        validNeighbourNode.parentNode = currentNode;

                        if (!isValidNeighbourNodeInOpenList)
                        {
                            openNodeList.Add(validNeighbourNode);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Returns the distance int between nodeA and nodeB
    /// </summary>
    private static int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs( (nodeA.gridPosition.x - nodeB.gridPosition.x) );
        int distY = Mathf.Abs( (nodeA.gridPosition.y - nodeB.gridPosition.y) );

        if(distX > distY)
        {
            // 10 used instead of 1, and 14 is a pythagoras approximation SQRT(10*10 + 10*10) - to avoid using floats
            return (14 * distY) + (10 * (distX - distY));
        }

        return (14 * distX) + (10 * (distY - distX));
    }

    /// <summary>
    /// Evaluate a neighbour node at neighboutNodeXPosition, neighbourNodeYPosition, using the
    /// specified gridNodes, closedNodeHashSet, and instantiated room.  Returns null if the node isn't valid
    /// </summary>
    private static Node GetValidNodeNeighbour(int neighbourNodeXPosition, int neighbourNodeYPosition, GridNodes gridNodes, HashSet<Node> closedNodeHashSet, InstantiatedRoom instantiatedRoom)
    {
        // if neighbour node position is beyond the grid then return null
        if( (neighbourNodeXPosition) >= ( (instantiatedRoom.room.templateUpperBounds.x) - (instantiatedRoom.room.templateLowerBounds.x) ) ||
            (neighbourNodeXPosition < 0) ||
            (neighbourNodeYPosition) >= ((instantiatedRoom.room.templateUpperBounds.y) - (instantiatedRoom.room.templateLowerBounds.y)) ||
            (neighbourNodeYPosition < 0) )
        {
            return null;
        }

        // Get neighbour node
        Node neighbourNode = gridNodes.GetGridNode(neighbourNodeXPosition, neighbourNodeYPosition);

        // check for obstacle at that position
        int movementPenaltyForGridSpace = instantiatedRoom.aStarMovementPenalty[neighbourNodeXPosition, neighbourNodeYPosition];

        // check for moveable obstacle at that position
        int itemObstacleForGridSpace = instantiatedRoom.aStarItemObstacles[neighbourNodeXPosition, neighbourNodeYPosition];

        // if neighbour is an obstacle or neighbour is in the closed list then skip
        if (movementPenaltyForGridSpace == 0 || itemObstacleForGridSpace == 0 || closedNodeHashSet.Contains(neighbourNode))
        {
            return null;
        }
        else
        {
            return neighbourNode;
        }
    }
}
