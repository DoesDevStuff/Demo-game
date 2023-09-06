using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]

public class InstantiatedRoom : MonoBehaviour
{
    [HideInInspector] public Room room;
    [HideInInspector] public Grid grid;
    [HideInInspector] public Tilemap floorTilemap;
    [HideInInspector] public Tilemap wallBehindTilemap;
    [HideInInspector] public Tilemap wallInfrontTilemap;
    [HideInInspector] public Tilemap collisionTilemap;
    [HideInInspector] public int[,] aStarMovementPenalty;  // use this 2d array to store movement penalties from the tilemaps to be used in AStar pathfinding
    [HideInInspector] public int[,] aStarItemObstacles; // use to store position of moveable items that are obstacles
    [HideInInspector] public Bounds roomColliderBounds;

    [SerializeField]
    private GameObject _environmentGameObject;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();

        roomColliderBounds = _boxCollider2D.bounds;
    }

    // Trigger room changed event when player enters a room
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Settings.playerTag)
        {
            room.isPreviouslyVisited = true;

            // Call room changed event
            StaticEventHandler.CallRoomChangedEvent(room);
        }
    }

    public void Initialise(GameObject roomGameObject)
    {
        // add doors to room, block off unused doorways, populate the tilemap, create obstacles, disable collision
    }
}
