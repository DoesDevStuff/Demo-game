using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName = "Scriptable Objects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    public bool displayInNodeGraphEditor = true;

    public bool isPassage;
    public bool isPassageNS;
    public bool isPassageEW;
    public bool isEntrance;
    public bool isBossRoom;
    public bool isNone;
}
