using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "Scriptable Objects/Dungeon/Dungeon Level")]
public class DungeonLevelSO : ScriptableObject
{
    public string levelName;
    public List<RoomTemplateSO> roomTemplateList;
    public List<RoomNodeGraphSO> roomNodeGraphList;

}
