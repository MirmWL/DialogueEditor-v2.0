using UnityEngine;

public class Drag : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseDrag;
    }
}