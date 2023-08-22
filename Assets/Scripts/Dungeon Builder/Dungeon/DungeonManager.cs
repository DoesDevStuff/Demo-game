using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ALL this can go in Game manager once I've rewritten some of the functions there
// for now it's in this

public class DungeonManager : SingletonMonobehaviour<DungeonManager>
{
    [SerializeField]
    private List<DungeonLevelSO> _dungeonLevelList;
    [SerializeField]
    private int _currentDungeonLevelListIndex = 0;

    private Room _currentRoom;
    private Room _previousRoom;

    private InstantiatedRoom _bossRoom;

    private void OnEnable()
    {
        // Subscribe to room changed event.
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;
    }
    private void OnDisable()
    {
        // Unsubscribe from room changed event
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
    }

    /// <summary>
    /// Handle room changed event
    /// </summary>
    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }

    public void SetCurrentRoom(Room room)
    {
        _previousRoom = _currentRoom;
        _currentRoom = room;
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        bool dungeonBuiltSucessfully = DungeonBuilder.Instance.GenerateDungeon(_dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSucessfully)
        {
            Debug.LogError("Couldn't build dungeon from specified rooms and node graphs");
        }

        StaticEventHandler.CallRoomChangedEvent(_currentRoom);

        /* TO DO
         * Get Player's position here
         */

        // Display Dungeon Level Text
        // TO DO
    }
    
}
