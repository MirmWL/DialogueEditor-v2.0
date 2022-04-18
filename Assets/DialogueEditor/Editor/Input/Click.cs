using UnityEngine;

public class Click : IInput
{
    public bool HasInput()
    {
        return Event.current.type == EventType.MouseDown;
    }
}