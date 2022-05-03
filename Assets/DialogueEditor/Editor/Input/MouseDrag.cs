using UnityEngine;

public class MouseDrag : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseDrag;
    }
}