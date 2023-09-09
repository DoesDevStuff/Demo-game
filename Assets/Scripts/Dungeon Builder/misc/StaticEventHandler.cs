using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
    // Room changed event
    public static event Action<RoomChangedEventArgs> OnRoomChanged;
    public static void CallRoomChangedEvent(Room room)
    {
        OnRoomChanged?.Invoke(new RoomChangedEventArgs() { room = room });
    }

    // Room enemies defeated event
    public static event Action<RoomEnemiesDefeatedArgs> OnRoomEnemiesDefeated;
    public static void CallRoomEnemiesDefeatedEvent(Room room)
    {
        OnRoomEnemiesDefeated?.Invoke(new RoomEnemiesDefeatedArgs() { room = room });
    }

    // TO DO:
    // 2. // Points scored event
    // 3. // Score changed event
    // 4. // Multiplier event
}

public class RoomChangedEventArgs : EventArgs
{
    public Room room;
}

public class RoomEnemiesDefeatedArgs : EventArgs
{
    public Room room;
}