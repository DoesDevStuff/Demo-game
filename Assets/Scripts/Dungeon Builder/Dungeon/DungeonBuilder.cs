using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
public class DungeonBuilder : SingletonMonobehaviour<DungeonBuilder>
{
    private bool _dungeonBuildSuccessful;
    public bool GenerateDungeon(DungeonLevelSO currentDungeonLevel)
    {
        return _dungeonBuildSuccessful;
    }
}
