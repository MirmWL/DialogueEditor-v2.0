using UnityEngine;

public class MouseUp : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseUp;
    }
}