using UnityEngine;

public class MouseClick : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseDown;
    }
}