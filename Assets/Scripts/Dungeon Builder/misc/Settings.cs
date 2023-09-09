using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Settings
{
    public const float pixelsPerUnit = 16f;
    public const float tileSizePixels = 16f;

    #region DUNGEON BUILD SETTINGS    
    public const int maxDungeonRebuildAttemptsForRoomGraph = 1000;
    public const int maxDungeonBuildAttempts = 10;
    #endregion

    #region ROOM SETTINGS
    public const int maxChildPassages = 3;
    public const float fadeInTime = 0.5f;
    public const float doorUnlockDelay = 1f;
    #endregion

    public const int defaultAStarMovementPenalty = 40;
    public const int preferredPathAStarMovementPenalty = 1;

    public const float playerMoveDistanceToRebuildPath = 3f;
    public const float enemyPathRebuildCooldown = 2f;

    public const int targetFrameRateToSpreadPathfindingOver = 60;

    #region GAMEOBJECT TAGS
    public const string playerTag = "Player";
    public const string playerWeapon = "playerWeapon";
    #endregion

    #region ANIMATOR PARAMETERS
    // Animator parameters - Door
    public static int open = Animator.StringToHash("open");
    public static int use = Animator.StringToHash("use");
    #endregion
}