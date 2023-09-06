using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    [HideInInspector] public GameState gameState;
    [HideInInspector] public GameState previousGameState;

    [SerializeField] private List<DungeonLevelSO> _dungeonLevelList;
    [SerializeField] private int _currentDungeonLevelListIndex = 0;

    private Room _currentRoom;
    private Room _previousRoom;

    private Player _player;

    private InstantiatedRoom _bossRoom;

    // TO DO:
    // find a way to get player details assuming we have more than 1 type of char? Maybe another SO
    // Then instantiate the player into the game using this


    private void OnEnable()
    {
        // Subscribe to room changed event.
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;

        // Subscribe to room enemies defeated event
        StaticEventHandler.OnRoomEnemiesDefeated += StaticEventHandler_OnRoomEnemiesDefeated;

        //TO DO:
        // Subscribe to the points scored event
        // Subscribe to score multiplier event
        

        // Subscribe to player destroyed event
        _player.destroyedEvent.OnDestroyed += Player_OnDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe from room changed event
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;

        // Unsubscribe from room enemies defeated event
        StaticEventHandler.OnRoomEnemiesDefeated -= StaticEventHandler_OnRoomEnemiesDefeated;

        // TO DO:
        // Unsubscribe from the points scored event
        // Unsubscribe from score multiplier event
       

        // Unubscribe from player destroyed event
        _player.destroyedEvent.OnDestroyed -= Player_OnDestroyed;

    }

    /// <summary>
    /// Handle room enemies defeated event
    /// </summary>
    private void StaticEventHandler_OnRoomEnemiesDefeated(RoomEnemiesDefeatedArgs roomEnemiesDefeatedArgs)
    {
        RoomEnemiesDefeated();
    }

    /// <summary>
    /// Handle player destroyed event
    /// </summary>
    private void Player_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        previousGameState = gameState;
        gameState = GameState.gameLost;
    }

    /// <summary>
    /// Handle room changed event
    /// </summary>
    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }


    private void Start()
    {
        previousGameState = GameState.gameStarted;
        gameState = GameState.gameStarted;

        // TO DO:
        // Set score to zero
        // Set multiplier to 1;
        // Set screen to black
    }

    public void SetCurrentRoom(Room room)
    {
        _previousRoom = _currentRoom;
        _currentRoom = room;
    }

    /// <summary>
    /// Room enemies defated - test if all dungeon rooms have been cleared of enemies - if so load
    /// next dungeon game level
    /// </summary>
    private void RoomEnemiesDefeated()
    {
        // TO DO:
        // Logic for this
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        bool dungeonBuiltSucessfully = DungeonBuilder.Instance.GenerateDungeon(_dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSucessfully)
        {
            Debug.LogError("Couldn't build dungeon from specified rooms and node graphs");
        }

        StaticEventHandler.CallRoomChangedEvent(_currentRoom);

        _player.gameObject.transform.position = new Vector3((_currentRoom.lowerBounds.x + _currentRoom.upperBounds.x) / 2, (_currentRoom.lowerBounds.y + _currentRoom.upperBounds.y) / 2, 0);

        _player.gameObject.transform.position = HelperUtilities.GetSpawnPositionNearestToPlayer(_player.gameObject.transform.position);

        //TO DO:
        // Display Dungeon Level Text 
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public Room GetCurrentRoom()
    {
        return _currentRoom;
    }

    public DungeonLevelSO GetCurrentDungeonLevel()
    {
        return _dungeonLevelList[_currentDungeonLevelListIndex];
    }
}
