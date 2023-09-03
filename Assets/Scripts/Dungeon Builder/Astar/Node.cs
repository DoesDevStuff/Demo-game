using System;
using UnityEngine;

// https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1?view=net-7.0&redirectedfrom=MSDN
// https://pavcreations.com/pathfinding-with-a-star-algorithm-in-unity-small-game-project/
// https://www.enjoyalgorithms.com/blog/a-star-search-algorithm

public class Node : IComparable<Node>
{
    public Vector2Int gridPosition;
    public int traverseNodeCost = 0; //cost of traversing from one node to another = g(n)
    public int heuristicApproxNodeCost = 0; // heuristic approximation of the node's value = h(n)

    public Node parentNode;

    public Node(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        parentNode = null;
    }

    public int FinalCost
    {
        get
        {
            return traverseNodeCost + heuristicApproxNodeCost;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        // compare will be <0 if this instance Fcost is less than nodeToCompare.FCost
        // compare will be >0 if this instance Fcost is greater than nodeToCompare.FCost
        // compare will be ==0 if the values are the same
        int compare = FinalCost.CompareTo(nodeToCompare.FinalCost);

        if(compare == 0)
        {
            compare = heuristicApproxNodeCost.CompareTo(nodeToCompare.heuristicApproxNodeCost);
        }
        return compare;
    }

}
