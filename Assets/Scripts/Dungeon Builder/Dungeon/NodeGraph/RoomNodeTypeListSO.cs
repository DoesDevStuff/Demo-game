using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeTypeListSO_", menuName = "Scriptable Objects/Dungeon/Room Node Type List")]
public class RoomNodeTypeListSO : ScriptableObject
{
    #region
    [Space(10)]
    [Header("ROOM NODE TYPE LIST")]
    #endregion

    #region
    [Tooltip("This should be populated with the RoomNodeTypeSO for the game - Using this in place of enum")]
    #endregion

    public List<RoomNodeTypeSO> list;
}