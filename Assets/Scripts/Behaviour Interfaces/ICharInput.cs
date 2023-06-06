using UnityEngine;
using UnityEngine.Events;

public interface ICharInput
{
    UnityEvent<Vector2> onMoveKeyPressed { get; set; }
    UnityEvent onShootKeyPressed { get; set; }
    UnityEvent onShootKeyReleased { get; set; }
    UnityEvent<Vector2> positionOfMouse { get; set; }
}