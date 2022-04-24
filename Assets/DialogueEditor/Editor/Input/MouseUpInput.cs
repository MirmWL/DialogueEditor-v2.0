using UnityEngine;

public class MouseUpInput : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseUp;
    }
}