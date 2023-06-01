using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture = null;
    private void Start()
    {
        SetCursorIcon();
    }

    /// <summary>
    /// Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    /// Here our hotspot is the point where we want it i.e the centre and in case the cursor is lopsided
    /// we will centre it basically.
    /// </summary>

    private void SetCursorIcon()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(_cursorTexture.width / 2f, _cursorTexture.height / 2f),
            CursorMode.Auto);
    }
}
