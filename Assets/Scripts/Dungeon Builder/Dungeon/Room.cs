using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string id;
    public string templateID;
    public GameObject prefab;
    public RoomNodeTypeSO roomNodeType;
    
    public Vector2Int lowerBounds;
    public Vector2Int upperBounds;
    public Vector2Int templateLowerBounds;
    public Vector2Int templateUpperBounds;
    public Vector2Int[] spawnPositionArray;
    public List<string> childRoomIDList;
    public string parentID;

    public InstantiatedRoom instantiatedRoom;

    public bool isClearOfEnemies = false;
    public bool isPreviouslyVisited = false;
    public bool isPositioned = false;

    public Room()
    {
        childRoomIDList = new List<string>();
        // doorway?
    }

    // TO DO

    /// <summary>
    /// Get the number of enemies to spawn for this room in this dungeon level
    /// </summary>
    /// 

    /// <summary>
    /// Get the room enemy spawn parameters for this dungeon level - if none found then return null
    /// </summary>

}
