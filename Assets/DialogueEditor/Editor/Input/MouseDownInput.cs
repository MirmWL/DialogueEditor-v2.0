using UnityEngine;

public class MouseDownInput : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseDown;
    }
}